using Sehaty_Plus.Domain.Entities;

namespace Sehaty_Plus.Application.Interfaces
{
    public interface IJwtProvider
    {
        (string token, DateTime expiresIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
