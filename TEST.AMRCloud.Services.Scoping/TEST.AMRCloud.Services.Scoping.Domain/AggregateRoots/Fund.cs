using TEST.AMRCloud.Services.Scoping.Domain.Entities;

namespace TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;

/// <summary>
/// Fund Aggregate Root.
/// Represents a fund with its related scoping and routine information.
/// </summary>
public class Fund : BaseEntity
{
    public string? FundName { get; set; }
    public string? FundCode { get; set; }
    public string? Status { get; set; }
    public decimal? Amount { get; set; }
    public int? EngagementId { get; set; }

    public ICollection<ScopingDetail> ScopingDetails { get; set; } = new List<ScopingDetail>();
    public ICollection<FundRoutineMapping> RoutineMappings { get; set; } = new List<FundRoutineMapping>();
}

/// <summary>
/// Routine entity for audit and scoping routines.
/// </summary>
public class Routine : BaseEntity
{
    public string? RoutineName { get; set; }
    public string? RoutineCode { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int? SequenceNumber { get; set; }

    public ICollection<FundRoutineMapping> FundMappings { get; set; } = new List<FundRoutineMapping>();
}

/// <summary>
/// Fund to Routine mapping for associating funds with routines.
/// </summary>
public class FundRoutineMapping : BaseEntity
{
    public int FundId { get; set; }
    public int RoutineId { get; set; }
    public string? Status { get; set; }
    public DateTime? CompletionDate { get; set; }

    public virtual Fund? Fund { get; set; }
    public virtual Routine? Routine { get; set; }
}

/// <summary>
/// Engagement Aggregate Root.
/// Represents an engagement with client and fund information.
/// </summary>
public class Engagement : BaseEntity
{
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? ClientName { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ICollection<Fund> Funds { get; set; } = new List<Fund>();
}
