using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Application.Common.EmailService;
using Sehaty_Plus.Application.Common.Interfaces;
using Sehaty_Plus.Application.Common.SmsService;
using Sehaty_Plus.Application.Common.SmsService.YourApp.Application.Interfaces.Services;
using Sehaty_Plus.Application.Feature.Auth.Services;
using Sehaty_Plus.Application.Feature.Roles.Services;
using Sehaty_Plus.Application.Services.Queries;
using Sehaty_Plus.Infrastructure.Persistence;
using Sehaty_Plus.Infrastructure.Repositories;
using Sehaty_Plus.Infrastructure.Services.Auth;
using Sehaty_Plus.Infrastructure.Services.Email;
using Sehaty_Plus.Infrastructure.Services.Role;
using Sehaty_Plus.Infrastructure.Services.Sms;
using System.Text;

namespace Sehaty_Plus.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.
                DbContextConfig(configuration)
                .AuthConfig(configuration);

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddScoped<IApplicationDbContext>(provider =>
                           provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IQueryExecuter, QueryExecuter>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IOtpService, OtpService>();
            services.AddTransient<ISmsService, TwilioSmsService>();

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


        private static IServiceCollection AuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            var setting = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = setting!.Issuer,
                    ValidAudience = setting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting!.Key))
                }
                ;
            });
            services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                }
                );
            services.AddSingleton<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
