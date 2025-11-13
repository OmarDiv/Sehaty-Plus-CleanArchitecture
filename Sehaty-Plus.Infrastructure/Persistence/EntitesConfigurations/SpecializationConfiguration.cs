using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sehaty_Plus.Infrastructure.Persistence.EntitesConfigurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Description)
                .HasMaxLength(300);
        }
    }
}
