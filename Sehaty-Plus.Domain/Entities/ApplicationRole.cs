using Microsoft.AspNetCore.Identity;

namespace Sehaty_Plus.Domain.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
