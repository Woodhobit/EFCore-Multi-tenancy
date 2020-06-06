using DAL.Model;
using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.DBContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        private readonly IConnectionStringProvider connectionStringProvider;

        public ApplicationContext(IConnectionStringProvider connectionStringProvider) : base()
        {
            this.connectionStringProvider = connectionStringProvider;
            // Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionStringProvider.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
