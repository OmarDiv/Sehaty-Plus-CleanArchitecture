using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class BranchConfigurations : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Primary key
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .IsRequired();
            builder.Property(b => b.Address)
                .IsRequired();
            builder.Property(b => b.PhoneNumber)
                .IsRequired();
            builder.HasMany(builder => builder.Doctors)
                .WithOne(doctor => doctor.Branch)
                .HasForeignKey(doctor => doctor.BranchId);
        }
    }
}
