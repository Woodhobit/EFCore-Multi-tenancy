using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Multi_tenancy
{
    public class HttpHeaderTenantProvider : ITenantProvider
    {
        private string tenantHeaderName = "tenantid";
        private string defaultTenantId = "defaultTenant";
        private readonly string tenantId;

        public HttpHeaderTenantProvider(IHttpContextAccessor accessor)
        {
            this.tenantId = accessor.HttpContext.Request.Headers[tenantHeaderName];

            if (string.IsNullOrEmpty(this.tenantId))
                this.tenantId = this.defaultTenantId;
        }

        public string GetTenantId()
        {
            return this.tenantId;
        }
    }
}
