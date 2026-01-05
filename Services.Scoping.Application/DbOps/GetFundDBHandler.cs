using MediatR;
using Services.Scoping.Application.Queries;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using Services.Scoping.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.DbOps
{
    public class GetFundDbHandler : IRequestHandler<GetAllFundsQuery, List<AssetManagementAnalysisDetails>>
    {
        private readonly IFundRepository _fundRepository;

        public GetFundDbHandler(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }   
        public async Task<List<AssetManagementAnalysisDetails>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
        {
            
            var result = await _fundRepository.GetAllFund(request.engagementId);
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new Exception("No Match Found");
            }
        }
    }
}
