// // -----------------------------------------------------------------------
// // <copyright file="SecurityContextFactory.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using Atlas.DatabaseContext;
using Atlas.Security.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Atlas.Security.DatabaseContext
{
    public class SecurityContextFactory : ISecurityContextFactory
    {
        private readonly IIdentityService _identityService;
        private readonly DbContextOptions<SecurityDbContext> _dbContextOptions;
        private readonly ILogger<SecurityDbContext> _log;

        public SecurityContextFactory(IIdentityService identityService, DbContextOptions<SecurityDbContext> dbContextOptions, ILogger<SecurityDbContext> log)
        {
            _identityService = identityService;
            _dbContextOptions = dbContextOptions;
            _log = log;
        }

        public virtual IContext Create()
        {
            return new SecurityDbContext(_identityService, _dbContextOptions, _log);
        }
    }
}