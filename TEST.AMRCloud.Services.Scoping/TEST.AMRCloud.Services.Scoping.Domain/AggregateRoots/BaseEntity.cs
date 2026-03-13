namespace TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;

/// <summary>
/// Base class for all domain entities with audit trail.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsActive { get; set; } = true;
}
