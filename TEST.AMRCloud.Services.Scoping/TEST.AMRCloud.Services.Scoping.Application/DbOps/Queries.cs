using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

/// <summary>
/// Query to retrieve engagement by ID.
/// </summary>
public class GetEngagementByIdQuery : IRequest<EngagementDto?>
{
    public int EngagementId { get; set; }

    public GetEngagementByIdQuery(int engagementId)
    {
        EngagementId = engagementId;
    }
}

/// <summary>
/// Query to get all engagements.
/// </summary>
public class GetAllEngagementsQuery : IRequest<IEnumerable<EngagementDto>>
{
}

/// <summary>
/// Query to get engagements by engagement manager.
/// </summary>
public class GetEngagementsByEngagementManagerQuery : IRequest<IEnumerable<EngagementDto>>
{
    public string EngagementManager { get; set; }

    public GetEngagementsByEngagementManagerQuery(string engagementManager)
    {
        EngagementManager = engagementManager;
    }
}

/// <summary>
/// Query to get engagement by code.
/// </summary>
public class GetEngagementByCodeQuery : IRequest<EngagementDto?>
{
    public string EngagementCode { get; set; }

    public GetEngagementByCodeQuery(string engagementCode)
    {
        EngagementCode = engagementCode;
    }
}

/// <summary>
/// Query to get engagement with its associated funds.
/// </summary>
public class GetEngagementWithFundsQuery : IRequest<EngagementWithFundsDto?>
{
    public int EngagementId { get; set; }

    public GetEngagementWithFundsQuery(int engagementId)
    {
        EngagementId = engagementId;
    }
}

/// <summary>
/// Query to get scoping details for a fund.
/// </summary>
public class GetScopingDetailsByFundQuery : IRequest<IEnumerable<ScopingDetailDto>>
{
    public int FundId { get; set; }

    public GetScopingDetailsByFundQuery(int fundId)
    {
        FundId = fundId;
    }
}
public class GetFundByIdQuery : IRequest<FundDto>
{
    public int Id { get; set; }
    public GetFundByIdQuery(int id) => Id = id;
}

public class GetAllFundsQuery : IRequest<List<FundDto>>
{
}

