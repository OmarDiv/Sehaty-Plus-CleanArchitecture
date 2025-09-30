namespace Sehaty_Plus.Application.Common.Authentication
{
    public interface IJwtProvider
    {
        (string token, DateTime expiresIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
