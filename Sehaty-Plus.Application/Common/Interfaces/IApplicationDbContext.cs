using Microsoft.EntityFrameworkCore;

namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Specialization> Specializations { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
