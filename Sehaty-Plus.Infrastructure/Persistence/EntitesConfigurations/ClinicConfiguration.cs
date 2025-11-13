using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .IsRequired();
            builder.Property(b => b.Address)
                .IsRequired();
            builder.Property(b => b.PhoneNumber)
                .IsRequired();
            builder.Property(x => x.Longitude)
                .HasPrecision(10, 6);
            builder.Property(x => x.Latitude)
                .HasPrecision(10, 6);
            builder.HasMany(builder => builder.DoctorClinics)
                .WithOne(x => x.Clinic);
        }
    }
}
