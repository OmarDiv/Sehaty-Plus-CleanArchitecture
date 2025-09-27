using Microsoft.AspNetCore.Identity;

namespace Sehaty_Plus.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}


