namespace TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;

/// <summary>
/// Base class for all domain entities with audit trail.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
}
