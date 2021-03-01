// // -----------------------------------------------------------------------
// // <copyright file="SecurityContextFactory.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using Atlas.DatabaseContext;
using Atlas.Security.User;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Atlas.Security.DatabaseContext
{
    public class SecurityContextFactory : ISecurityContextFactory
    {
        private readonly IIdentityService _identityService;
        private readonly IDbContextOptions _contextOptions;
        private readonly ILogger _log;

        public SecurityContextFactory(IIdentityService identityService, IDbContextOptions contextOptions, ILogger log)
        {
            _identityService = identityService;
            _contextOptions = contextOptions;
            _log = log;
        }

        public virtual IContext Create()
        {
            return new SecurityDbContext(_identityService, _contextOptions, _log);
        }
    }
}