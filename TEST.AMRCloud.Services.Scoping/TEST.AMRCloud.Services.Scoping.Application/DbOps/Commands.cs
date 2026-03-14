using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

/// <summary>
/// Command to create a new engagement.
/// </summary>
public class CreateEngagementCommand : IRequest<EngagementDto?>
{
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? EngagementManager { get; set; }
    public string? EngagementPartner { get; set; }
    public DateTime? PeriodEndDate { get; set; }

    public CreateEngagementCommand(EngagementDto engagementDto)
    {
        EngagementName = engagementDto.EngagementName;
        EngagementCode = engagementDto.EngagementCode;
        EngagementManager = engagementDto.EngagementManager;
        EngagementPartner = engagementDto.EngagementPartner;
        PeriodEndDate = engagementDto.PeriodEndDate;
    }

    public CreateEngagementCommand(string? engagementName, string? engagementCode, 
        string? engagementManager, string? engagementPartner, DateTime? periodEndDate)
    {
        EngagementName = engagementName;
        EngagementCode = engagementCode;
        EngagementManager = engagementManager;
        EngagementPartner = engagementPartner;
        PeriodEndDate = periodEndDate;
    }
}

/// <summary>
/// Command to update an existing engagement.
/// </summary>
public class UpdateEngagementCommand : IRequest<EngagementDto?>
{
    public int EngagementId { get; set; }
    public string? EngagementName { get; set; }
    public string? EngagementCode { get; set; }
    public string? EngagementManager { get; set; }
    public string? EngagementPartner { get; set; }
    public DateTime? PeriodEndDate { get; set; }

    public UpdateEngagementCommand(EngagementDto engagementDto)
    {
        EngagementId = engagementDto.Id;
        EngagementName = engagementDto.EngagementName;
        EngagementCode = engagementDto.EngagementCode;
        EngagementManager = engagementDto.EngagementManager;
        EngagementPartner = engagementDto.EngagementPartner;
        PeriodEndDate = engagementDto.PeriodEndDate;
    }

    public UpdateEngagementCommand(int engagementId, string? engagementName, string? engagementCode,
        string? engagementManager, string? engagementPartner, DateTime? periodEndDate)
    {
        EngagementId = engagementId;
        EngagementName = engagementName;
        EngagementCode = engagementCode;
        EngagementManager = engagementManager;
        EngagementPartner = engagementPartner;
        PeriodEndDate = periodEndDate;
    }
}

/// <summary>
/// Command to delete an engagement.
/// </summary>
public class DeleteEngagementCommand : IRequest<bool>
{
    public int EngagementId { get; set; }

    public DeleteEngagementCommand(int engagementId)
    {
        EngagementId = engagementId;
    }
}
