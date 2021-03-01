// // -----------------------------------------------------------------------
// // <copyright file="IContext.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Atlas.DatabaseContext
{
    public interface IContext : IDisposable
    {
        IDatabaseAccessor DatabaseAccessor { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry Entry(object entity);
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
        void SetState(object entity, EntityState state);
    }
}