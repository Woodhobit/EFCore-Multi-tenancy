namespace Infrastructure.Multi_tenancy.Contracts
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}
