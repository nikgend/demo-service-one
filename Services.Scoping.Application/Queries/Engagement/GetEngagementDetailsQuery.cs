using MediatR;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.Queries.Engagement
{
    public record GetEngagementDetailsQuery() : IRequest<List<EngagementDetails>>;
}
