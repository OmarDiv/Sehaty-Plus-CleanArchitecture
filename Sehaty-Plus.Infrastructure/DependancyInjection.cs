using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sehaty_Plus.Application.Interfaces;
using Sehaty_Plus.Domain.Entities;
using Sehaty_Plus.Infrastructure.Persistence;
using Sehaty_Plus.Infrastructure.Security.Authentication;

namespace Sehaty_Plus.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.
                DbContextConfig(configuration)
                .AuthConfig();


            return services;
        }
        private static IServiceCollection DbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("Sehaty-Plus.Infrastructure");
                    sqlOptions.EnableRetryOnFailure();
                });
            });
            return services;
        }


        private static IServiceCollection AuthConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddSingleton<IJwtProvider, JwtProvider>();

            return services;
        }


    }
}
