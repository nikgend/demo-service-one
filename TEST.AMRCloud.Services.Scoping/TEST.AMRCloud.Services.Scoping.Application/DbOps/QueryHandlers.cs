using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

/// <summary>
/// Handler for GetFundByIdQuery.
/// Retrieves fund information from the database and maps to DTO.
/// </summary>
public class GetFundByIdQueryHandler : IRequestHandler<GetFundByIdQuery, FundDto?>
{
    private readonly IFundRepository _fundRepository;

    public GetFundByIdQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto?> Handle(GetFundByIdQuery request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.FundId);

        if (fund == null)
            return null;

        return MapToDto(fund);
    }

    private FundDto MapToDto(Domain.AggregateRoots.Fund fund)
    {
        return new FundDto
        {
            Id = fund.Id,
            FundName = fund.FundName,
            FundCode = fund.FundCode,
            Status = fund.Status,
            Amount = fund.Amount,
            EngagementId = fund.EngagementId,
            CreatedDate = fund.CreatedDate,
            CreatedBy = fund.CreatedBy
        };
    }
}

/// <summary>
/// Handler for GetAllFundsQuery.
/// </summary>
public class GetAllFundsQueryHandler : IRequestHandler<GetAllFundsQuery, IEnumerable<FundDto>>
{
    private readonly IFundRepository _fundRepository;

    public GetAllFundsQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<IEnumerable<FundDto>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
    {
        var funds = await _fundRepository.GetAllAsync();
        return funds.Select(MapToDto).ToList();
    }

    private FundDto MapToDto(Domain.AggregateRoots.Fund fund)
    {
        return new FundDto
        {
            Id = fund.Id,
            FundName = fund.FundName,
            FundCode = fund.FundCode,
            Status = fund.Status,
            Amount = fund.Amount,
            EngagementId = fund.EngagementId,
            CreatedDate = fund.CreatedDate,
            CreatedBy = fund.CreatedBy
        };
    }
}

/// <summary>
/// Handler for GetEngagementByIdQuery.
/// </summary>
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
/// Handler for GetRoutineByIdQuery.
/// </summary>
public class GetRoutineByIdQueryHandler : IRequestHandler<GetRoutineByIdQuery, RoutineDto?>
{
    private readonly IRoutineRepository _routineRepository;

    public GetRoutineByIdQueryHandler(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;
    }

    public async Task<RoutineDto?> Handle(GetRoutineByIdQuery request, CancellationToken cancellationToken)
    {
        var routine = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routine == null)
            return null;

        return MapToDto(routine);
    }

    private RoutineDto MapToDto(Domain.AggregateRoots.Routine routine)
    {
        return new RoutineDto
        {
            Id = routine.Id,
            RoutineName = routine.RoutineName,
            RoutineCode = routine.RoutineCode,
            Description = routine.Description,
            Status = routine.Status,
            SequenceNumber = routine.SequenceNumber,
            CreatedDate = routine.CreatedDate,
            CreatedBy = routine.CreatedBy
        };
    }
}

/// <summary>
/// Handler for GetScopingDetailsByFundQuery.
/// </summary>
public class GetScopingDetailsByFundQueryHandler : IRequestHandler<GetScopingDetailsByFundQuery, IEnumerable<ScopingDetailDto>>
{
    private readonly ScopingContext _context;

    public GetScopingDetailsByFundQueryHandler(ScopingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScopingDetailDto>> Handle(GetScopingDetailsByFundQuery request, CancellationToken cancellationToken)
    {
        var details = await _context.ScopingDetails
            .AsNoTracking()
            .Where(s => s.FundId == request.FundId)
            .ToListAsync();

        return details.Select(MapToDto).ToList();
    }

    private ScopingDetailDto MapToDto(Domain.Entities.ScopingDetail detail)
    {
        return new ScopingDetailDto
        {
            Id = detail.Id,
            FundId = detail.FundId,
            Description = detail.Description,
            Scope = detail.Scope,
            Status = detail.Status,
            Observations = detail.Observations,
            SequenceNumber = detail.SequenceNumber,
            CreatedDate = detail.CreatedDate,
            CreatedBy = detail.CreatedBy
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
/// Handler for GetEngagementsByStatusQuery.
/// </summary>
public class GetEngagementsByStatusQueryHandler : IRequestHandler<GetEngagementsByStatusQuery, IEnumerable<EngagementDto>>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetEngagementsByStatusQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<IEnumerable<EngagementDto>> Handle(GetEngagementsByStatusQuery request, CancellationToken cancellationToken)
    {
        var engagements = await _engagementRepository.GetEngagementsByStatusAsync(request.Status);
        return engagements.Select(MapToDto).ToList();
    }

    private EngagementDto MapToDto(Domain.AggregateRoots.Engagement engagement)
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
/// Handler for GetEngagementWithFundsQuery.
/// </summary>
public class GetEngagementWithFundsQueryHandler : IRequestHandler<GetEngagementWithFundsQuery, EngagementWithFundsDto?>
{
    private readonly IEngagementRepository _engagementRepository;

    public GetEngagementWithFundsQueryHandler(IEngagementRepository engagementRepository)
    {
        _engagementRepository = engagementRepository;
    }

    public async Task<EngagementWithFundsDto?> Handle(GetEngagementWithFundsQuery request, CancellationToken cancellationToken)
    {
        var engagement = await _engagementRepository.GetEngagementWithFundsAsync(request.EngagementId);

        if (engagement == null)
            return null;

        return MapToDto(engagement);
    }

    private EngagementWithFundsDto MapToDto(Domain.AggregateRoots.Engagement engagement)
    {
        return new EngagementWithFundsDto
        {
            Id = engagement.Id,
            EngagementName = engagement.EngagementName,
            EngagementCode = engagement.EngagementCode,
            ClientName = engagement.ClientName,
            Status = engagement.Status,
            StartDate = engagement.StartDate,
            EndDate = engagement.EndDate,
            CreatedDate = engagement.CreatedDate,
            CreatedBy = engagement.CreatedBy,
            Funds = engagement.Funds?.Select(f => new FundDto
            {
                Id = f.Id,
                FundName = f.FundName,
                FundCode = f.FundCode,
                Status = f.Status,
                Amount = f.Amount,
                EngagementId = f.EngagementId,
                CreatedDate = f.CreatedDate,
                CreatedBy = f.CreatedBy
            }).ToList() ?? new List<FundDto>()
        };
    }
}
