using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.LicenseNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(e => e.LicenseNumber)
                   .IsUnique();

            builder.Property(e => e.Education)
                   .HasMaxLength(500);

            builder.Property(e => e.Biography)
                   .HasMaxLength(1000);

            builder.HasIndex(e => e.UserId)
                   .IsUnique();

            builder.HasMany(x => x.DoctorClinics)
                .WithOne(x => x.Doctor);

            builder.HasOne(d => d.User)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserId);

            builder.HasOne(d => d.Specialization)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecializationId);
        }
    }
}
