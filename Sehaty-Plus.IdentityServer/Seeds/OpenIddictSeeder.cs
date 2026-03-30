using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Sehaty_Plus.IdentityServer.Seeds;

public class OpenIddictSeeder(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var appManager = scope.ServiceProvider
            .GetRequiredService<IOpenIddictApplicationManager>();

        var scopeManager = scope.ServiceProvider
            .GetRequiredService<IOpenIddictScopeManager>();

        // -----------------------------------------------
        // 1. Register the "sehaty-api" scope
        //    This scope represents access to your API
        // -----------------------------------------------
        if (await scopeManager.FindByNameAsync("sehaty-api", cancellationToken) is null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = "sehaty-api",
                DisplayName = "Sehaty Plus API Access",
                // These are the resources (audiences) that accept this scope
                // Your API will validate that tokens have this audience
                Resources = { "https://localhost:5001" }
            }, cancellationToken);
        }

        // -----------------------------------------------
        // 2. Register the frontend SPA as a client
        //    This represents your React/Angular/Vue app
        // -----------------------------------------------
        if (await appManager.FindByClientIdAsync("sehaty-web", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "sehaty-web",
                ClientType = ClientTypes.Public, // SPA = public client (no secret)
                DisplayName = "Sehaty Plus Web App",

                // Where the Auth server can redirect back after login
                RedirectUris =
                {
                    new Uri("https://localhost:5173/callback"),
                    new Uri("https://localhost:5173/silent-renew")
                },

                // Where the Auth server can redirect after logout
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:5173/")
                },

                // What this client is allowed to request
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.Endpoints.EndSession,

                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,

                    Permissions.ResponseTypes.Code,

                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    $"{Permissions.Prefixes.Scope}openid",
                    $"{Permissions.Prefixes.Scope}offline_access",
                    $"{Permissions.Prefixes.Scope}sehaty-api"
                },

                // PKCE required — rejects requests without code_challenge
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            }, cancellationToken);
        }

        // -----------------------------------------------
        // 3. Register Swagger/Scalar as a client
        //    So you can test OAuth2 flows from your API docs
        // -----------------------------------------------
        if (await appManager.FindByClientIdAsync("sehaty-swagger", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "sehaty-swagger",
                ClientType = ClientTypes.Public,
                DisplayName = "Sehaty Plus Swagger UI",

                RedirectUris =
                {
                    new Uri("https://localhost:5001/swagger/oauth2-redirect.html")
                },

                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:5001/swagger/")
                },

                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,

                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,

                    Permissions.ResponseTypes.Code,

                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    $"{Permissions.Prefixes.Scope}openid",
                    $"{Permissions.Prefixes.Scope}sehaty-api"
                },

                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}