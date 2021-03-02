// // -----------------------------------------------------------------------
// // <copyright file="SecurityDbContext.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using Atlas.DatabaseContext;
using Atlas.Security.Models;
using Atlas.Security.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Atlas.Security.DatabaseContext
{
    public class SecurityDbContext : Context
    {
        private static IConfiguration _configuration;

        // Required for entity framework migrations.
        public SecurityDbContext(IDbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public SecurityDbContext(IIdentityService identityService, IDbContextOptions dbContextOptions, ILogger log)
            : base(dbContextOptions, identityService, log)
        {
        }

        public virtual DbSet<Login> Logins { get; set; }
    }
}