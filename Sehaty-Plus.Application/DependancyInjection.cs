using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Application.Common.Behaviors;
using Sehaty_Plus.Application.Common.SmsService;

namespace Sehaty_Plus.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependancyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.Configure<TwilioSettings>(configuration.GetSection(TwilioSettings.SectionName));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}