namespace TEST.AMRCloud.Services.Scoping.Domain.Contracts;

/// <summary>
/// Data Transfer Object for Fund.
/// </summary>
public class FundDto
{
    public int Id { get; set; }
    public string? FundName { get; set; }
    public string? FundCode { get; set; }
    public string? EngagementManager { get; set; }
    public decimal? Amount { get; set; }
    public int? EngagementId { get; set; }
}

/// <summary>
/// Data Transfer Object for Engagement.
/// </summary>
public class EngagementDto
{
    public int Id { get; set; }
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? EngagementManager { get; set; }
    public string? EngagementPartner { get; set; }
    public DateTime? PeriodEndDate { get; set; }
}

/// <summary>
/// Data Transfer Object for Engagement with associated Funds.
/// </summary>
public class EngagementWithFundsDto
{
    public int Id { get; set; }
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? EngagementManager { get; set; }
    public string? EngagementPartner { get; set; }
    public DateTime? PeriodEndDate { get; set; }
    public ICollection<FundDto> Funds { get; set; } = new List<FundDto>();
}

/// <summary>
/// Data Transfer Object for Routine.
/// </summary>
public class RoutineDto
{
    public int Id { get; set; }
    public string? RoutineName { get; set; }
    public string? RoutineCode { get; set; }
    public string? Description { get; set; }
    public string? EngagementManager { get; set; }
    public int? SequenceNumber { get; set; }
}

/// <summary>
/// DTO for ScopingDetail.
/// </summary>
public class ScopingDetailDto
{
    public int Id { get; set; }
    public int FundId { get; set; }
    public string? Description { get; set; }
    public string? Scope { get; set; }
    public string? EngagementManager { get; set; }
    public string? Observations { get; set; }
    public int? SequenceNumber { get; set; }
}
