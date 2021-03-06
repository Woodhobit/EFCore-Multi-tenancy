using API.OperationFilters;
using BLL.Manager;
using DAL.DBContext;
using DAL.Model;
using DAL.Repository;
using Infrastructure.Configuration;
using Infrastructure.Multi_tenancy;
using Infrastructure.Multi_tenancy.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));

            services.AddDbContext<ApplicationContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantProvider, HttpHeaderTenantProvider>();
            services.AddScoped<IDataBaseManager, SimpleDataBaseManager>();
            services.AddScoped<IConnectionStringProvider, DatabaseBasedConnectionStringProvider>();

            services.AddScoped<IRepository<UserProfile>, Repository<UserProfile>>();
            services.AddScoped<IUserProfileManager, UserProfileManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API V1",
                    Version = "v1",
                    Description = "Demo: Multi-tenancy",
                });

                c.OperationFilter<TenantHeaderOperationFilter>();
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Airport distance API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
