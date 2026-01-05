using MediatR;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.Queries
{
    public record GetAllFundsQuery(int engagementId) : IRequest<List<AssetManagementAnalysisDetails>>;
}
