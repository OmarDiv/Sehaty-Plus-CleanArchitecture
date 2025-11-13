using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sehaty_Plus.Infrastructure.Persistence.Data;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
       [
                new ApplicationRole
                {
                 Id= DefaultRoles.Admin.Id,
                 Name = DefaultRoles.Admin.Name,
                 NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
                 ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
                },
                new ApplicationRole
                {
                 Id= DefaultRoles.Member.Id,
                 Name= DefaultRoles.Member.Name,
                 NormalizedName= DefaultRoles.Member.Name.ToUpper(),
                 ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp,
                 IsDefault= true
                },
                new ApplicationRole
                {
                 Id= DefaultRoles.Patient.Id,
                 Name = DefaultRoles.Patient.Name,
                 NormalizedName = DefaultRoles.Patient.Name.ToUpper(),
                 ConcurrencyStamp = DefaultRoles.Patient.ConcurrencyStamp,
                },
                new ApplicationRole
                {
                 Id= DefaultRoles.Doctor.Id,
                 Name = DefaultRoles.Doctor.Name,
                 NormalizedName = DefaultRoles.Doctor.Name.ToUpper(),
                 ConcurrencyStamp = DefaultRoles.Doctor.ConcurrencyStamp,
                },
            ]);


        }
    }
}
