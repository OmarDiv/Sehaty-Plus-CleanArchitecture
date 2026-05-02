using Hajj.Domain.Shared.Interfaces;
using Sehaty_Plus.Domain.Entities;

namespace Hajj.Domain.Shared
{
    public class BaseEntity : IActivable, IEntity
    {
        public long Id { get; set; }
        public long? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastUpdatedById { get; set; }
        public ApplicationUser? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool IsActive { get; set; } = true;

        public void SetDeleted(long? currentUser)
        {
            LastUpdatedById = currentUser;
            LastUpdatedOn = DateTime.UtcNow;
        }
        public void SetLastUpdatedOn(long? currentUser)
        {
            LastUpdatedById = currentUser;
            LastUpdatedOn = DateTime.UtcNow;
        }
        public void SetCreatedOn(long? currentUser)
        {
            CreatedById = currentUser;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
