using MediatR;
using Services.Scoping.Application.Queries.Engagement;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.DbOps.Engagement
{
    public class GetEngagementDetailsDBHandler : IRequestHandler<GetEngagementDetailsQuery, List<EngagementDetails>>
    {
        private readonly IEngagementRepository _engagementRepository;

        public GetEngagementDetailsDBHandler(IEngagementRepository engagementRepository)
        {
            _engagementRepository = engagementRepository;
        }
        public async Task<List<EngagementDetails>> Handle(GetEngagementDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = Task.FromResult(await _engagementRepository.GetEngagementDetailsAsync());
            if (result.Result.Count > 0)
            {
                return result.Result;
            }
            else
            {
                throw new Exception("No Match Found.");
            }
        }
    }
}
