using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Contracts.Engagement;
using Services.Scoping.Domain.Entities.engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Infrastructure.Repositories.Engagement
{

    public class EngagementCreationService : IEngagementCreationService
    {
        private readonly IEngagementRepository _engagementRepository;

        public EngagementCreationService(IEngagementRepository engagementRepository)
        {
            _engagementRepository = engagementRepository;
        }
        public async Task<int> CreateEngagement(CreateEngagementRequest request, CancellationToken cancellationToken)
        {
           int engagementid =  await _engagementRepository.UnitOfWork.SaveEngagementEntitiesAsync(request, cancellationToken);
           return engagementid;
        }
    }
}
