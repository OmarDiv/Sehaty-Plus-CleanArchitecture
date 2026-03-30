using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Sehaty_Plus.Domain.Entities;
using Sehaty_Plus.IdentityServer.Data;
using Sehaty_Plus.IdentityServer.Seeds;

var builder = WebApplication.CreateBuilder(args);
// =============================================
// 1. DATABASE
// =============================================
builder.Services.AddDbContext<IdentityServerDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("Sehaty-Plus.IdentityServer")
    );
    // Critical: tells EF Core that OpenIddict
    // will configure its own entities
    options.UseOpenIddict();
});

// =============================================
// 2. ASP.NET CORE IDENTITY
// =============================================
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<IdentityServerDbContext>()
.AddDefaultTokenProviders();

// =============================================
// 3. OPENIDDICT
// =============================================
builder.Services.AddOpenIddict()

    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<IdentityServerDbContext>();
    })

    .AddServer(options =>
    {
        // OAuth2 / OIDC endpoints this server exposes
        options.SetAuthorizationEndpointUris("/connect/authorize")
               .SetTokenEndpointUris("/connect/token")
               .SetUserInfoEndpointUris("/connect/userinfo")
               .SetEndSessionEndpointUris("/connect/logout")
               .SetIntrospectionEndpointUris("/connect/introspect");

        // Only Authorization Code + Refresh Token
        // No ROPC — we are learning the correct modern flow
        options.AllowAuthorizationCodeFlow()
               .AllowRefreshTokenFlow();

        // Enforce PKCE — clients without code_challenge are rejected
        options.RequireProofKeyForCodeExchange();

        // Scopes this server recognizes
        options.RegisterScopes(
            OpenIddictConstants.Scopes.OpenId,
            OpenIddictConstants.Scopes.Email,
            OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.OfflineAccess,
            "sehaty-api"  // custom scope your API will validate against
        );

        // Token lifetimes
        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
        options.SetRefreshTokenLifetime(TimeSpan.FromDays(14));
        options.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(5));

        // Development certificates — replace with real X.509 in production
        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();

        // Use ASP.NET Core as the host (handles HTTP requests)
        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough()
               .EnableUserInfoEndpointPassthrough()
               .EnableEndSessionEndpointPassthrough();
    })

    .AddValidation(options =>
    {
        // This Auth server also validates its own tokens
        // (needed for the userinfo endpoint)
        options.UseLocalServer();
        options.UseAspNetCore();
    });

// =============================================
// 4. MVC — for Login page and Authorization UI
// =============================================
builder.Services.AddControllersWithViews();

// =============================================
// 5. CORS — allow your frontend and API
// =============================================
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                builder.Configuration
                       .GetSection("AllowedOrigins")
                       .Get<string[]>()!)
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// =============================================
// 6. HOSTED SERVICE — seeds clients and scopes
//    on startup (explained fully below)
// =============================================
builder.Services.AddHostedService<OpenIddictSeeder>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();