using Infrastructure.Configuration;
using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.Extensions.Options;

namespace Infrastructure.Multi_tenancy
{
    public class DatabaseBasedConnectionStringProvider : IConnectionStringProvider
    {
        private readonly ITenantProvider tenantProvider;
        private readonly IDataBaseManager dataBaseManager;
        private readonly DatabaseOptions options;
        private string databaseNamePlaceholder = "{tenantDbName}";

        public DatabaseBasedConnectionStringProvider(
            ITenantProvider tenantProvider, 
            IDataBaseManager dataBaseManager,
            IOptions<DatabaseOptions> options)
        {
            this.tenantProvider = tenantProvider;
            this.dataBaseManager = dataBaseManager;
            this.options = options.Value;
        }

        public string GetConnectionString()
        {
            var tenantId = this.tenantProvider.GetTenantId();
            var dbName = this.dataBaseManager.GetDataBaseName(tenantId);
            var connectionString = options.ConnectionStringTemplate.Replace(databaseNamePlaceholder, dbName);

            return connectionString;
        }
    }
}
