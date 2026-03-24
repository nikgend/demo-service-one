using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TEST.AMRCloud.Services.Scoping.Domain.Common;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

public class GetEngagementByIdQueryHandler : IRequestHandler<GetEngagementByIdQuery, EngagementDto?>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetEngagementByIdQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<EngagementDto?> Handle(GetEngagementByIdQuery request, CancellationToken cancellationToken)
    {
        var engagement = await _engagementRepository.GetByIdAsync(request.EngagementId);

        if (engagement == null)
            return null;

        return MapToDto(engagement);
    }

    private EngagementDto MapToDto(Domain.AggregateRoots.Engagement engagement)
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
/// Handler for GetAllEngagementsQuery.
/// </summary>
public class GetAllEngagementsQueryHandler : IRequestHandler<GetAllEngagementsQuery, IEnumerable<EngagementDto>>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetAllEngagementsQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<IEnumerable<EngagementDto>> Handle(GetAllEngagementsQuery request, CancellationToken cancellationToken)
    {
        var engagements = await _engagementRepository.GetAllAsync();
        return engagements.Select(MapToDto).ToList();
    }

    private EngagementDto MapToDto(Domain.AggregateRoots.Engagement engagement)
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
/// Handler for GetEngagementsByEngagementManagerQuery.
/// </summary>
public class GetEngagementsByEngagementManagerQueryHandler : IRequestHandler<GetEngagementsByEngagementManagerQuery, IEnumerable<EngagementDto>>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetEngagementsByEngagementManagerQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<IEnumerable<EngagementDto>> Handle(GetEngagementsByEngagementManagerQuery request, CancellationToken cancellationToken)
    {
        var engagements = await _engagementRepository.GetEngagementsByEngagementManagerAsync(request.EngagementManager);
        return engagements.Select(MapToDto).ToList();
    }

    private EngagementDto MapToDto(Domain.AggregateRoots.Engagement engagement)
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
/// Handler for GetEngagementByCodeQuery.
/// </summary>
public class GetEngagementByCodeQueryHandler : IRequestHandler<GetEngagementByCodeQuery, EngagementDto?>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetEngagementByCodeQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<EngagementDto?> Handle(GetEngagementByCodeQuery request, CancellationToken cancellationToken)
    {
        var engagement = await _engagementRepository.GetEngagementByCodeAsync(request.EngagementCode);

        if (engagement == null)
            return null;

        return MapToDto(engagement);
    }

    private EngagementDto MapToDto(Domain.AggregateRoots.Engagement engagement)
    {
        return new EngagementDto
        {
            Id = engagement.Id,
            EngagementName = engagement.EngagementName,
            EngagementCode = engagement.EngagementCode,
            EngagementManager = engagement.EngagementManager,
            EngagementPartner = engagement.EngagementPartner,
            PeriodEndDate = engagement.PeriodEndDate,
        };
    }
}

public class GetFundByIdQueryHandler : IRequestHandler<GetFundByIdQuery, FundDto>
{
    private readonly IFundRepository _fundRepository;

    public GetFundByIdQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto> Handle(GetFundByIdQuery request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.Id, cancellationToken);
        if (fund == null)
            throw new NotFoundException($"Fund with Id {request.Id} not found.");

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

public class GetAllFundsQueryHandler : IRequestHandler<GetAllFundsQuery, List<FundDto>>
{
    private readonly IFundRepository _fundRepository;

    public GetAllFundsQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<List<FundDto>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
    {
        var funds = await _fundRepository.GetAllAsync(cancellationToken);

        return funds.Select(f => new FundDto
        {
            Id = f.Id,
            FundCode = f.FundCode,
            FundName = f.FundName,
            Description = f.Description,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt
        }).ToList();
    }
}

