using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sehaty_Plus.Domain.Entities;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Sehaty_Plus.IdentityServer.Controllers;

public class AuthorizationController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IOpenIddictApplicationManager applicationManager,
    IOpenIddictScopeManager scopeManager) : Controller
{
    // -----------------------------------------------
    // GET /connect/authorize
    //
    // Step 2 in the flow diagram:
    // The frontend redirected the browser here.
    // We check if the user is logged in.
    // If not → show login page.
    // If yes → issue authorization code and redirect back.
    // -----------------------------------------------
    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OpenIddict request cannot be retrieved.");

        // Is the user already authenticated (has a session cookie)?
        var result = await HttpContext.AuthenticateAsync(
            IdentityConstants.ApplicationScheme);

        if (!result.Succeeded)
        {
            // Not logged in — redirect to our Login page
            // ReturnUrl tells the login page where to come back after login
            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType
                            ? Request.Form.ToList()
                            : Request.Query.ToList())
                });
        }

        // User is logged in — retrieve their full record
        var user = await userManager.GetUserAsync(result.Principal)
            ?? throw new InvalidOperationException("The user cannot be retrieved.");

        // Build the claims identity that will be encoded into the tokens
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        // Subject claim — the unique identifier for this user in all tokens
        identity.SetClaim(Claims.Subject, await userManager.GetUserIdAsync(user))
                .SetClaim(Claims.Email, await userManager.GetEmailAsync(user))
                .SetClaim(Claims.Name, user.FirstName + " " + user.LastName)
                .SetClaim(Claims.GivenName, user.FirstName)
                .SetClaim(Claims.FamilyName, user.LastName);

        // Add roles as claims
        var roles = await userManager.GetRolesAsync(user);
        identity.SetClaims(Claims.Role, [.. roles]);

        // Add permissions as claims
        // This is how your HasPermission attribute continues to work
        var permissions = await GetUserPermissionsAsync(user);
        identity.SetClaims("permissions", [.. permissions]);

        // Tell OpenIddict which scopes were granted
        identity.SetScopes(request.GetScopes());

        // Tell OpenIddict which claims go into which token
        // access_token → what your API reads
        // identity_token → what the frontend reads about the user
        identity.SetDestinations(GetDestinations);

        return SignIn(new ClaimsPrincipal(identity),
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    // -----------------------------------------------
    // POST /connect/token
    //
    // Step 5 in the flow diagram:
    // Frontend exchanges the authorization code for tokens.
    // OpenIddict validates the code and PKCE verifier,
    // then calls this method to build the final token response.
    // -----------------------------------------------
    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OpenIddict request cannot be retrieved.");

        if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
        {
            // Retrieve the claims principal stored in the authorization code
            // or refresh token (OpenIddict does this automatically)
            var result = await HttpContext.AuthenticateAsync(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var userId = result.Principal?.GetClaim(Claims.Subject);
            var user = await userManager.FindByIdAsync(userId!);

            if (user is null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                            Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The token is no longer valid."
                    }));
            }

            // Make sure the user is still allowed to sign in
            if (!await signInManager.CanSignInAsync(user))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                            Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The user is no longer allowed to sign in."
                    }));
            }

            var identity = new ClaimsIdentity(
                result.Principal!.Claims,
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: Claims.Name,
                roleType: Claims.Role);

            // Refresh claims in case roles/permissions changed since the code was issued
            identity.SetClaim(Claims.Subject, await userManager.GetUserIdAsync(user))
                    .SetClaim(Claims.Email, await userManager.GetEmailAsync(user))
                    .SetClaim(Claims.Name, user.FirstName + " " + user.LastName);

            var roles = await userManager.GetRolesAsync(user);
            identity.SetClaims(Claims.Role, [.. roles]);

            var permissions = await GetUserPermissionsAsync(user);
            identity.SetClaims("permissions", [.. permissions]);

            identity.SetDestinations(GetDestinations);

            return SignIn(new ClaimsPrincipal(identity),
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }

    // -----------------------------------------------
    // GET /connect/userinfo
    //
    // The frontend or API can call this endpoint
    // with a valid access token to get user profile info.
    // -----------------------------------------------
    [HttpGet("~/connect/userinfo")]
    public async Task<IActionResult> Userinfo()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
            return Challenge(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            [Claims.Subject] = await userManager.GetUserIdAsync(user)
        };

        if (User.HasScope(Scopes.Email))
        {
            claims[Claims.Email] = await userManager.GetEmailAsync(user) ?? string.Empty;
            claims[Claims.EmailVerified] = user.EmailConfirmed;
        }

        if (User.HasScope(Scopes.Profile))
        {
            claims[Claims.GivenName] = user.FirstName;
            claims[Claims.FamilyName] = user.LastName;
            claims["gender"] = user.Gender.ToString();
            claims["is_active"] = user.IsActive;
        }

        return Ok(claims);
    }

    // -----------------------------------------------
    // POST /connect/logout
    // -----------------------------------------------
    [HttpPost("~/connect/logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return SignOut(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties
            {
                RedirectUri = "/"
            });
    }

    // -----------------------------------------------
    // PRIVATE HELPERS
    // -----------------------------------------------

    // Decides which claims go into which token type
    // This is important for security and size
    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        return claim.Type switch
        {
            Claims.Subject
                => [Destinations.AccessToken, Destinations.IdentityToken],

            Claims.Name or Claims.GivenName or Claims.FamilyName
                when claim.Subject!.HasScope(Scopes.Profile)
                => [Destinations.AccessToken, Destinations.IdentityToken],

            Claims.Email or Claims.EmailVerified
                when claim.Subject!.HasScope(Scopes.Email)
                => [Destinations.AccessToken, Destinations.IdentityToken],

            Claims.Role
                => [Destinations.AccessToken],

            "permissions"
                => [Destinations.AccessToken],

            _ => [Destinations.AccessToken]
        };
    }

    private async Task<IEnumerable<string>> GetUserPermissionsAsync(ApplicationUser user)
    {
        // Reuse your existing permission logic:
        // Get roles → get claims for those roles
        var userRoles = await userManager.GetRolesAsync(user);

        // We need the DbContext here to query role claims
        // inject IApplicationDbContext or the AuthDbContext directly
        // For now returning empty — we will wire this properly in Phase 2
        return [];
    }
}