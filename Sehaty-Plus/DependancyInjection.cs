using Dapper;
using Hangfire;
using Microsoft.AspNetCore.RateLimiting;
using Sehaty_Plus.Application.Services.TypeHandlers;
using Sehaty_Plus.Errors;
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
            services.AddOpenApi();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddBackgroundJobsConfig(configuration);
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
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
