using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(c => c.Latitude)
                .HasPrecision(9, 6);

            builder.Property(c => c.Longitude)
                .HasPrecision(9, 6);
            builder.HasMany(builder => builder.DoctorClinics)
                .WithOne(x => x.Clinic);
        }
    }
}
