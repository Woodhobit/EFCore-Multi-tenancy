using Infrastructure.Configuration;
using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure.Multi_tenancy
{
    public class ServerBasedConnectionStringProvider : IConnectionStringProvider
    {
        private readonly ITenantProvider tenantProvider;
        private readonly DatabaseOptions options;

        public ServerBasedConnectionStringProvider(
            ITenantProvider tenantProvider,
            IOptions<DatabaseOptions> options)
        {
            this.tenantProvider = tenantProvider;
            this.options = options.Value;
        }

        public string GetConnectionString()
        {
            var tenantId = this.tenantProvider.GetTenantId();

            return this.options.ConnectionStrings[tenantId];
        }
    }
}
