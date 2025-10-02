using Microsoft.AspNetCore.Identity;


namespace Sehaty_Plus.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //public bool IsDisabled { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}


