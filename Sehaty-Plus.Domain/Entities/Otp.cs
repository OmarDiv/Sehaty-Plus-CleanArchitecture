using System;
using System.Collections.Generic;
using System.Linq;
namespace Sehaty_Plus.Domain.Entities
{
    public class Otp
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;
    }
}