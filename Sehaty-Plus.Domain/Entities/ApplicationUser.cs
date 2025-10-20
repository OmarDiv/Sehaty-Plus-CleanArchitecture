using Microsoft.AspNetCore.Identity;
using Sehaty_Plus.Domain.Enums;
namespace Sehaty_Plus.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public string? ProfilePicture { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public ICollection<Otp> Otps { get; set; } = [];
    }
}


