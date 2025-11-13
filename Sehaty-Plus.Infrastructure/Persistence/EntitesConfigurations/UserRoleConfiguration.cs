using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sehaty_Plus.Infrastructure.Persistence.Data;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = DefaultUsers.Admin.Id,
                RoleId = DefaultRoles.Admin.Id
            });
        }
    }
}
