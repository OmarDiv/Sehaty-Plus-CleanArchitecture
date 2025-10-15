using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sehaty_Plus.Application.Common.Interfaces;
using Sehaty_Plus.Domain.Common;
using System.Reflection;
using System.Security.Claims;
namespace Sehaty_Plus.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Otp> Otps { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var cascade = modelBuilder.Model.GetEntityTypes()
              .SelectMany(t => t.GetForeignKeys())
              .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);
        foreach (var fk in cascade)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entityEntry in entries)
        {
            var CurrentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(e => e.CreatedById).CurrentValue = CurrentUserId!;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(e => e.UpdatedById).CurrentValue = CurrentUserId;
                entityEntry.Property(e => e.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
