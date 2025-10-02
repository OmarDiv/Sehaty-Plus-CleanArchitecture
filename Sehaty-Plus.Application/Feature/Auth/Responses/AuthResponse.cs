namespace Sehaty_Plus.Application.Feature.Auth.Responses
{
    public record AuthResponse(
     string Id,
     string? Email,
     string FirstName,
     string LastName,
     string Token,
     int ExpiresIn,
     string RefreshToken,
     DateTime RefreshTokenExpiration);
}