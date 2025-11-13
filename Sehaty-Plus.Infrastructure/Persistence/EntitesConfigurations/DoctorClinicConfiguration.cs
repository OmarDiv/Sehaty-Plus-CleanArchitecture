using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class DoctorClinicConfiguration : IEntityTypeConfiguration<DoctorClinic>
    {
        public void Configure(EntityTypeBuilder<DoctorClinic> builder)
        {
            builder.Property(dc => dc.ConsultationFee)
                   .HasPrecision(10, 2);
        }
    }
}
