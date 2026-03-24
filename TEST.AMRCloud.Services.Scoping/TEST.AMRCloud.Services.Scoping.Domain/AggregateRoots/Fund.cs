using TEST.AMRCloud.Services.Scoping.Domain.Entities;

namespace TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;


/// <summary>
/// Engagement Aggregate Root.
/// Represents an engagement with partner and manager information.
/// </summary>
public class Engagement : BaseEntity
{
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? EngagementManager { get; set; }
    public string? EngagementPartner { get; set; }
    public DateTime? PeriodEndDate { get; set; }

    //public ICollection<Fund> Funds { get; set; } = new List<Fund>();
}
