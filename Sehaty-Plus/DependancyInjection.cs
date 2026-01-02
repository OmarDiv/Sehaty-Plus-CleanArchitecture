using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Sehaty_Plus.Errors;
using Sehaty_Plus.Infrastructure.Persistence.Data;
using System.Threading.RateLimiting;
namespace Sehaty_Plus
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!));
            });
            services.AddEndpointsApiExplorer().AddOpenApi();
            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("ApiAdminPolicy", policy =>
                        policy.RequireRole(DefaultRoles.Admin.Name));
                }
            );

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddBackgroundJobsConfig(configuration);
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddPolicy("ipLimit", httpcontext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                       partitionKey: httpcontext.Connection.RemoteIpAddress?.ToString(),
                       factory: _ => new FixedWindowRateLimiterOptions
                       {
                           PermitLimit = 5,
                           Window = TimeSpan.FromMinutes(1)
                       })
                    );
            });

            return services;
        }

        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();
            return services;
        }
    }
}
