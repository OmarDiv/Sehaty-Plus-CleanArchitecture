namespace Sehaty_Plus.Domain.Entities
{
    public class Otp
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public ApplicationUser User { get; set; } = default!;
    }
}