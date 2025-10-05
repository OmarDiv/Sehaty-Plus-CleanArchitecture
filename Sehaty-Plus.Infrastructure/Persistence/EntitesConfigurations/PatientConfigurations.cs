using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.NationalId)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(e => e.NationalId)
                .IsUnique();

            builder.HasIndex(e => e.UserId)
                .IsUnique();

            builder.Property(e => e.BloodType)
                .HasMaxLength(5);

            builder.Property(e => e.EmergencyContact)
                .HasMaxLength(20);

            builder.HasOne(p => p.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(x => x.UserId);

        }
    }
}