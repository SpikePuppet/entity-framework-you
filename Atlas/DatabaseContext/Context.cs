// // -----------------------------------------------------------------------
// // <copyright file="Context.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Extensions;
using Atlas.Model.Attributes;
using Atlas.Model.Enumerations;
using Atlas.Model.Models;
using Atlas.Security.User;
using Atlas.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Atlas.DatabaseContext
{
    public class Context : DbContext, IContext
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _log;

        public Context(IDbContextOptions databaseOptions, IIdentityService identityService, ILogger log)
            : this(databaseOptions)
        {
            _identityService = identityService;
            _log = log;
            DatabaseAccessor = new DatabaseAccessor(Database);
        }

        public Context(IDbContextOptions databaseName)
            : base((DbContextOptions)databaseName)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }

        public IDatabaseAccessor DatabaseAccessor { get; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var changedEntities = ChangeTracker.Entries().ToList();
            var originalStates = changedEntities.Select(x => x.State).ToList();

            var result = base.SaveChanges();

            SaveAudit(changedEntities, originalStates);

            return result;
        }

        public Task<int> SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();

            var changedEntities = ChangeTracker.Entries().ToList();
            var originalStates = changedEntities.Select(x => x.State).ToList();

            var result = base.SaveChangesAsync();

            SaveAudit(changedEntities, originalStates);

            return result;
        }

        public void SetState(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var foreignKeysToRemoveCascadeDeleteFrom
                = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys())
                .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var foreignKey in foreignKeysToRemoveCascadeDeleteFrom)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            base.OnModelCreating(modelBuilder);
        }

        protected virtual void SaveAudit(IEnumerable<EntityEntry> dbEntityEntries, IEnumerable<EntityState> originalEntityStates)
        {
            var entitiesWithOriginalStates = dbEntityEntries.Zip(originalEntityStates, (x, y) => new {x.Entity, State = y});

            var audits = entitiesWithOriginalStates
                .Where(x => x.State != EntityState.Unchanged && x.State != EntityState.Detached &&
                            x.Entity.GetType().IsDefined(typeof(AuditMeAttribute), false)).Select(x => CreateAudit(x.Entity, x.State)).ToList();

            Audits.AddRange(audits);
            base.SaveChanges();
        }

        protected Audit CreateAudit(object entity, EntityState entityState)
        {
            if (entity.GetType().GetPrimaryKeyProperty().PropertyType == typeof(long))
            {
                throw new ApplicationException("Cannot Audit a class with a primary key of type long");
            }

            var auditAction = entityState switch
            {
                EntityState.Added => AuditAction.Add,
                EntityState.Deleted => AuditAction.Delete,
                EntityState.Modified => AuditAction.Update,
                _ => AuditAction.Fetch
            };

            return new Audit
            {
                AuditAction = auditAction,
                DateTime = DateTime.UtcNow,
                Email = _identityService.GetEmail(),
                Type = entity.GetType().FullName,
                TypeId = (int) entity.GetIdFromEntity(),
                SerializedEntity = entity.SerializeToJson()
            };
        }
    }
}