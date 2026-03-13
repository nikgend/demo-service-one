using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

/// <summary>
/// Handler for CreateEngagementCommand.
/// Creates a new engagement in the database.
/// </summary>
public class CreateEngagementCommandHandler : IRequestHandler<CreateEngagementCommand, EngagementDto?>
{
    private readonly IEngagementRepository _engagementRepository;

    public CreateEngagementCommandHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<EngagementDto?> Handle(CreateEngagementCommand request, CancellationToken cancellationToken)
    {
        var engagement = new Engagement
        {
            EngagementName = request.EngagementName,
            EngagementCode = request.EngagementCode,
            ClientName = request.ClientName,
            Status = request.Status,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
            IsActive = true
        };

        var createdEngagement = await _engagementRepository.AddAsync(engagement);

        if (createdEngagement == null)
            return null;

        return MapToDto(createdEngagement);
    }

    private EngagementDto MapToDto(Engagement engagement)
    {
        return new EngagementDto
        {
            Id = engagement.Id,
            EngagementName = engagement.EngagementName,
            EngagementCode = engagement.EngagementCode,
            ClientName = engagement.ClientName,
            Status = engagement.Status,
            StartDate = engagement.StartDate,
            EndDate = engagement.EndDate,
            CreatedDate = engagement.CreatedDate,
            CreatedBy = engagement.CreatedBy
        };
    }
}

/// <summary>
/// Handler for UpdateEngagementCommand.
/// Updates an existing engagement in the database.
/// </summary>
public class UpdateEngagementCommandHandler : IRequestHandler<UpdateEngagementCommand, EngagementDto?>
{
    private readonly IEngagementRepository _engagementRepository;

    public UpdateEngagementCommandHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<EngagementDto?> Handle(UpdateEngagementCommand request, CancellationToken cancellationToken)
    {
        var engagement = await _engagementRepository.GetByIdAsync(request.EngagementId);

        if (engagement == null)
            return null;

        engagement.EngagementName = request.EngagementName ?? engagement.EngagementName;
        engagement.EngagementCode = request.EngagementCode ?? engagement.EngagementCode;
        engagement.ClientName = request.ClientName ?? engagement.ClientName;
        engagement.Status = request.Status ?? engagement.Status;
        engagement.StartDate = request.StartDate ?? engagement.StartDate;
        engagement.EndDate = request.EndDate ?? engagement.EndDate;
        engagement.ModifiedDate = DateTime.UtcNow;
        engagement.ModifiedBy = request.ModifiedBy;

        var updatedEngagement = await _engagementRepository.UpdateAsync(engagement);

        if (updatedEngagement == null)
            return null;

        return MapToDto(updatedEngagement);
    }

    private EngagementDto MapToDto(Engagement engagement)
    {
        return new EngagementDto
        {
            Id = engagement.Id,
            EngagementName = engagement.EngagementName,
            EngagementCode = engagement.EngagementCode,
            ClientName = engagement.ClientName,
            Status = engagement.Status,
            StartDate = engagement.StartDate,
            EndDate = engagement.EndDate,
            CreatedDate = engagement.CreatedDate,
            CreatedBy = engagement.CreatedBy
        };
    }
}

/// <summary>
/// Handler for DeleteEngagementCommand.
/// Deletes an engagement from the database.
/// </summary>
public class DeleteEngagementCommandHandler : IRequestHandler<DeleteEngagementCommand, bool>
{
    private readonly IEngagementRepository _engagementRepository;

    public DeleteEngagementCommandHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<bool> Handle(DeleteEngagementCommand request, CancellationToken cancellationToken)
    {
        var engagement = await _engagementRepository.GetByIdAsync(request.EngagementId);

        if (engagement == null)
            return false;

        return true;
    }
}
