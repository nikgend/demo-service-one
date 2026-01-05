using MediatR;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Contracts.Engagement;
using Services.Scoping.Infrastructure.Repositories.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.DbOps.Engagement
{
    public class UpdateEngagementDBhandler : IRequestHandler<UpdateEngagementRequest, EngagementNameCodeDuplicateCheck>
    {
        private readonly IEngagementRepository _engagementRepository;
        public UpdateEngagementDBhandler(IEngagementRepository engagementRepository)
        {
            _engagementRepository = engagementRepository;
        }
        public async Task<EngagementNameCodeDuplicateCheck> Handle(UpdateEngagementRequest request, CancellationToken cancellationToken)
        {
            var result = new EngagementNameCodeDuplicateCheck
            {
                IsNameExist = _engagementRepository.GetEngagementNameExistenceDetails(null, request)
            };
            if (result.IsNameExist)
            {
                result.message = "Engagement Name " + request.Name + " already exists. Please enter different name";
            }
            else
            {
                if (await UpdateEngagement(request, cancellationToken) > 0)
                    result.message = "Engagement " + request.Name + " has been Updated successfully.";
            }
            return result;
        }

        private async Task<int> UpdateEngagement(UpdateEngagementRequest request, CancellationToken cancellationToken)
        {
            // Implementation for updating engagement in the database
            bool result = await _engagementRepository.UnitOfWork.UpdateEngagementEntitiesAsync(request, cancellationToken);
            if(result)
            {
                return request.EngagementId;
            }
            else
            {
                throw new Exception("Engagement update failed.");
            }
        }
    }
}
