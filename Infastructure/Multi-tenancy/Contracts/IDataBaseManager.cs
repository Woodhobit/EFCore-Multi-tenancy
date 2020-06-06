namespace Infrastructure.Multi_tenancy.Contracts
{
    public interface IDataBaseManager
    {
        string GetDataBaseName(string tenantId);
    }
}
