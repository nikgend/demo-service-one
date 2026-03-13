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
    public string? ClientName { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CreatedBy { get; set; }

    public CreateEngagementCommand(EngagementDto engagementDto)
    {
        EngagementName = engagementDto.EngagementName;
        EngagementCode = engagementDto.EngagementCode;
        ClientName = engagementDto.ClientName;
        Status = engagementDto.Status;
        StartDate = engagementDto.StartDate;
        EndDate = engagementDto.EndDate;
        CreatedBy = engagementDto.CreatedBy;
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
    public string? ClientName { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ModifiedBy { get; set; }

    public UpdateEngagementCommand(EngagementDto engagementDto)
    {
        EngagementId = engagementDto.Id;
        EngagementName = engagementDto.EngagementName;
        EngagementCode = engagementDto.EngagementCode;
        ClientName = engagementDto.ClientName;
        Status = engagementDto.Status;
        StartDate = engagementDto.StartDate;
        EndDate = engagementDto.EndDate;
        ModifiedBy = engagementDto.CreatedBy;
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
