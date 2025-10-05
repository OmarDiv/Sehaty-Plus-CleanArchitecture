using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Primary key
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasDefaultValueSql("NEWID()");

            // License Number
            builder.Property(e => e.LicenseNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(e => e.LicenseNumber)
                   .IsUnique();

            // Consultation Fee
            builder.Property(e => e.ConsultationFee)
                   .HasPrecision(18, 2);

            // Education
            builder.Property(e => e.Education)
                   .HasMaxLength(500);

            // Biography
            builder.Property(e => e.Biography)
                   .HasMaxLength(1000);

            builder.HasIndex(e => e.UserId)
                   .IsUnique();

            builder.HasOne(d => d.Branch)
                    .WithMany(b => b.Doctors)
                    .HasForeignKey(d => d.BranchId);

            builder.HasOne(d => d.User)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserId);
            //// Relation with Specialization
            //builder.HasOne(d => d.Specialization)
            //       .WithMany(s => s.Doctors)
            //       .HasForeignKey(d => d.SpecializationId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
