using Hajj.Domain.Shared.Interfaces;
using Sehaty_Plus.Domain.Common;

namespace Sehaty_Plus.Domain.Entities;

public class Specialization : AuditableEntity, IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Doctor> Doctors { get; set; } = [];
}
