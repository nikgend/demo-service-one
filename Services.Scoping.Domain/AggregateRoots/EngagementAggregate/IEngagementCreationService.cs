using Services.Scoping.Domain.Contracts.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.EngagementAggregate
{
    public interface IEngagementCreationService
    {
        Task<int> CreateEngagement(CreateEngagementRequest request, CancellationToken cancellationToken);
    }
}
