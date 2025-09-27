using Microsoft.EntityFrameworkCore;
namespace Sehaty_Plus.Domain.Entities
{
    [Owned]
    public class RefreshToken
    {

        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    }
}
