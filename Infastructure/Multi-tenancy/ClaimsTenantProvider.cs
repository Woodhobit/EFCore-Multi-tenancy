using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Infrastructure.Multi_tenancy
{
    public class ClaimsTenantProvider : ITenantProvider
    {
        private string tenantClaimName = "tenantid";
        private string defaultTenantId = "defaultTenant";
        private readonly string tenantId;

        public ClaimsTenantProvider(IHttpContextAccessor accessor)
        {
            var tenantClaim = accessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == this.tenantClaimName);

            if (tenantClaim != null)
            {
                this.tenantId = tenantClaim.Value;
            }
            else 
            {
                this.tenantId = this.defaultTenantId;
            }
        }

        public string GetTenantId()
        {
            return this.tenantId;
        }
    }
}
