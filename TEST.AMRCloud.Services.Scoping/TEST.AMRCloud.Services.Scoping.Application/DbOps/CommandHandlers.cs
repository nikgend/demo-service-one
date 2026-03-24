using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Domain.Common;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;
using TEST.AMRCloud.Services.Scoping.Domain.Entities;
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
        try
        {
            var engagement = new Engagement
            {
                EngagementName = request.EngagementName,
                EngagementCode = request.EngagementCode,
                EngagementManager = request.EngagementManager,
                EngagementPartner = request.EngagementPartner,
                PeriodEndDate = request.PeriodEndDate
            };

            var createdEngagement = await _engagementRepository.AddAsync(engagement);

            if (createdEngagement == null)
                return null;
            return MapToDto(createdEngagement);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private EngagementDto MapToDto(Engagement engagement)
    {
        return new EngagementDto
        {
            Id = engagement.Id,
            EngagementName = engagement.EngagementName,
            EngagementCode = engagement.EngagementCode,
            EngagementManager = engagement.EngagementManager,
            EngagementPartner = engagement.EngagementPartner,
            PeriodEndDate = engagement.PeriodEndDate
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
        engagement.EngagementManager = request.EngagementManager ?? engagement.EngagementManager;
        engagement.EngagementPartner = request.EngagementPartner ?? engagement.EngagementPartner;
        engagement.PeriodEndDate = request.PeriodEndDate ?? engagement.PeriodEndDate;

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
            EngagementManager = engagement.EngagementManager,
            EngagementPartner = engagement.EngagementPartner,
            PeriodEndDate = engagement.PeriodEndDate
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
        try
        {
            var engagement = await _engagementRepository.GetByIdAsync(request.EngagementId);

            if (engagement == null)
                return false;

            var result = await _engagementRepository.DeleteAsync(request.EngagementId);

            if (result)
            {
                await _engagementRepository.SaveChangesAsync();
            }

            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

public class CreateFundCommandHandler : IRequestHandler<CreateFundCommand, FundDto>
{
    private readonly IFundRepository _fundRepository;

    public CreateFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto> Handle(CreateFundCommand request, CancellationToken cancellationToken)
    {
        var fund = new Fund
        {
            FundCode = request.FundCode,
            FundName = request.FundName,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _fundRepository.AddAsync(fund, cancellationToken);

        return new FundDto
        {
            Id = fund.Id,
            FundCode = fund.FundCode,
            FundName = fund.FundName,
            Description = fund.Description,
            CreatedAt = fund.CreatedAt,
            UpdatedAt = fund.UpdatedAt
        };
    }
}

public class UpdateFundCommandHandler : IRequestHandler<UpdateFundCommand, FundDto>
{
    private readonly IFundRepository _fundRepository;

    public UpdateFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto> Handle(UpdateFundCommand request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.Id, cancellationToken);
        if (fund == null)
            throw new NotFoundException($"Fund with Id {request.Id} not found.");

        fund.FundCode = request.FundCode;
        fund.FundName = request.FundName;
        fund.Description = request.Description;
        fund.UpdatedAt = DateTime.UtcNow;

        await _fundRepository.UpdateAsync(fund, cancellationToken);

        return new FundDto
        {
            Id = fund.Id,
            FundCode = fund.FundCode,
            FundName = fund.FundName,
            Description = fund.Description,
            CreatedAt = fund.CreatedAt,
            UpdatedAt = fund.UpdatedAt
        };
    }
}

public class DeleteFundCommandHandler : IRequestHandler<DeleteFundCommand, bool>
{
    private readonly IFundRepository _fundRepository;

    public DeleteFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<bool> Handle(DeleteFundCommand request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.Id, cancellationToken);
        if (fund == null)
            throw new NotFoundException($"Fund with Id {request.Id} not found.");

        await _fundRepository.DeleteAsync(fund, cancellationToken);
        return true;
    }
}

