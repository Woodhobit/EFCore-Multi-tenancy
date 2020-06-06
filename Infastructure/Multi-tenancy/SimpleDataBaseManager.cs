using Infrastructure.Multi_tenancy.Contracts;
using System.Collections.Generic;

namespace Infrastructure.Multi_tenancy
{
    public class SimpleDataBaseManager : IDataBaseManager
    {
        // IMPORTANT NOTICE: The solution uses simple dictionary for demo purposes.
        // The Best "Real-life" solutions would be creating 'RootDataBase' with 
        // all Tenants Parameters/Options like: TenantName, DatabaseName, other configuration.
        private readonly Dictionary<string, string> tenantConfigurationDictionary = new Dictionary<string, string>
        {
            {
                "defaultTenant", "DemoDB"
            },
            {
                "tenant-1", "DemoDB-ten1"
            },
                        {
                "tenant-2", "DemoDB-ten2"
            }

        };

        // IMPORTANT NOTICE: solution does not validate permission to access specified tenant
        // The Best "Real-life" solutions would be validation permission
        public string GetDataBaseName(string tenantId)
        {
            return this.tenantConfigurationDictionary[tenantId];
        }
    }
}
