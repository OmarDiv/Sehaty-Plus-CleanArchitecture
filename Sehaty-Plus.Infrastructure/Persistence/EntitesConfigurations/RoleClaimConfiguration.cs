using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sehaty_Plus.Infrastructure.Persistence.Data;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class RoleClaims : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            //DefaultData
            var permissions = Permissions.GetAllPermissions();
            var adminClaims = new List<IdentityRoleClaim<long>>();

            adminClaims = permissions.Select((p, i) => new IdentityRoleClaim<long>
            {
                Id = i + 1,
                ClaimType = Permissions.Type,
                ClaimValue = p,
                RoleId = DefaultRoles.Admin.Id
            }).ToList();

            builder.HasData(adminClaims);
        }
    }
}
