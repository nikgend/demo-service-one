using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;

namespace TEST.AMRCloud.Services.Scoping.Domain.Entities;

/// <summary>
/// ScopingDetail entity represents detailed scoping information for a fund.
/// </summary>
public class ScopingDetail
{
    public int Id { get; set; }
    public int FundId { get; set; }
    public string? Description { get; set; }
    public string? Scope { get; set; }
    public string? EngagementManager { get; set; }
    public string? Observations { get; set; }
    public int? SequenceNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation property
    public virtual Fund? Fund { get; set; }
}
