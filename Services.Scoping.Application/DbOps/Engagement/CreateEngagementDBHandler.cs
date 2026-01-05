using MediatR;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Contracts.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.DbOps.Engagement
{
    public class CreateEngagementDBHandler : IRequestHandler<CreateEngagementRequest, EngagementNameCodeDuplicateCheck>
    {
        private readonly IEngagementRepository _engagementRepository;
        private readonly IEngagementCreationService _engagementCreationService;

        public CreateEngagementDBHandler(IEngagementRepository engagementRepository, IEngagementCreationService engagementCreationService)
        {
            _engagementRepository = engagementRepository;
            _engagementCreationService = engagementCreationService;
        }
        public async Task<EngagementNameCodeDuplicateCheck> Handle(CreateEngagementRequest request, CancellationToken cancellationToken)
        {
            var result = new EngagementNameCodeDuplicateCheck
            {
                IsNameExist = _engagementRepository.GetEngagementNameExistenceDetails(request, null)
            };
            if (result.IsNameExist)
            {
                result.message = "Engagement Name " + request.Name + " already exists. Please enter different name";
            }
            else
            {
                int engagementId = await _engagementCreationService.CreateEngagement(request, cancellationToken);
                result.EngagementId = engagementId.ToString();
                result.Name = request.Name;
                result.ValidateNewEngagement = true;
                result.message = "Engagement created successfully with ID: " + engagementId;
            }
            return result;
        }
    }
}
