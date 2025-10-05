using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Errors;
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


            return services;
        }
    }
}
