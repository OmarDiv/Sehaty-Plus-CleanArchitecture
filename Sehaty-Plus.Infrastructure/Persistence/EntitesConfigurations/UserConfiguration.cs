using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sehaty_Plus.Domain.Enums;
using Sehaty_Plus.Infrastructure.Persistence.Data;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");

            builder.Property(x => x.FirstName)
                .HasMaxLength(100);
            builder.Property(x => x.LastName)
                .HasMaxLength(100);
            builder.HasData(new ApplicationUser
            {
                Id = DefaultUsers.Admin.Id,
                Email = DefaultUsers.Admin.Email,
                FirstName = "Sehaty Plus",
                LastName = "Admin",
                UserName = DefaultUsers.Admin.Email,
                NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
                NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
                SecurityStamp = DefaultUsers.Admin.SecurityStamp,
                ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.Admin.PasswordHased,
                RegisteredDate = new DateOnly(2025, 11, 12),
                Gender = Gender.Male,
                IsActive = true,
            });

        }
    }
}
