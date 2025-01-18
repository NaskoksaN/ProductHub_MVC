using Microsoft.EntityFrameworkCore;
using ProductHub.DataAccess;
using ProductHub.DataAccess.Data;
using ProductHub.Utility.Interface;
using ProductHub.Utility.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration
                        .GetConnectionString("DefaultConnection") 
                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

    }
}
