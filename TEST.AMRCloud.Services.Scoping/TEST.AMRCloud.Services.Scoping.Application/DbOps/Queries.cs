using MediatR;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;

namespace TEST.AMRCloud.Services.Scoping.Application.DbOps;

/// <summary>
/// Query to retrieve a fund by its ID.
/// </summary>
public class GetFundByIdQuery : IRequest<FundDto?>
{
    public int FundId { get; set; }

    public GetFundByIdQuery(int fundId)
    {
        FundId = fundId;
    }
}

/// <summary>
/// Query to retrieve all funds.
/// </summary>
public class GetAllFundsQuery : IRequest<IEnumerable<FundDto>>
{
}

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
/// Query to get routine information.
/// </summary>
public class GetRoutineByIdQuery : IRequest<RoutineDto?>
{
    public int RoutineId { get; set; }

    public GetRoutineByIdQuery(int routineId)
    {
        RoutineId = routineId;
    }
}

/// <summary>
/// Query to get all engagements.
/// </summary>
public class GetAllEngagementsQuery : IRequest<IEnumerable<EngagementDto>>
{
}

/// <summary>
/// Query to get engagements by status.
/// </summary>
public class GetEngagementsByStatusQuery : IRequest<IEnumerable<EngagementDto>>
{
    public string Status { get; set; }

    public GetEngagementsByStatusQuery(string status)
    {
        Status = status;
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
