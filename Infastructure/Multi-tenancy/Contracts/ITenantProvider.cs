namespace Infrastructure.Multi_tenancy.Contracts
{
    public interface ITenantProvider
    {
        string GetTenantId();
    }
}
