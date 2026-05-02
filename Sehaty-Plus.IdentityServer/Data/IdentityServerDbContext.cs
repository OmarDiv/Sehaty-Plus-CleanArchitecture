using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sehaty_Plus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.IdentityServer.Data
{
    public class IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseOpenIddict();

            // 1. Resolve Cascade Cycles for Auditable Entities
            // Many entities (like Specialization) have a required relationship with AspNetUsers (CreatedBy).
            // This causes multiple cascade paths from AspNetUsers to Doctor.
            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                if (relationship.DeleteBehavior == DeleteBehavior.Cascade &&
                    (relationship.Properties.Any(p => p.Name == "CreatedById" || p.Name == "UpdatedById")))
                {
                    relationship.DeleteBehavior = DeleteBehavior.NoAction;
                }
            }

            // 2. Fix Decimal Precision Warnings
            builder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.Latitude).HasPrecision(12, 10);
                entity.Property(e => e.Longitude).HasPrecision(12, 10);
            });

            builder.Entity<DoctorClinic>(entity =>
            {
                entity.Property(e => e.ConsultationFee).HasPrecision(18, 2);
            });
        }
    }
}
