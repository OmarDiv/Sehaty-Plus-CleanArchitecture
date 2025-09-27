using Sehaty_Plus.Errors;
using Sehaty_Plus.Infrastructure.Security.Authentication;
namespace Sehaty_Plus
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddOpenApi();
   
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
