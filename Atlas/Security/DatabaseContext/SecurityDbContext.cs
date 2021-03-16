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
using Microsoft.Extensions.Logging;

namespace Atlas.Security.DatabaseContext
{
    public class SecurityDbContext : Context
    {
        public SecurityDbContext(IIdentityService identityService, DbContextOptions<SecurityDbContext> dbContextOptions, ILogger<SecurityDbContext> log)
            : base(dbContextOptions, identityService, log)
        {
        }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }


        public virtual DbSet<Login> Logins { get; set; }
    }
}