using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
