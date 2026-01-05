using MediatR;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using Services.Scoping.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Application.DbOps
{
    public class CreateFundDbHandler : IRequestHandler<CreateFundRequest, int>
    {
        private readonly IFundRepository _fundRepository;
        public CreateFundDbHandler(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }
        public async Task<int> Handle(CreateFundRequest request, CancellationToken cancellationToken)
        {
            int saveResult = await _fundRepository.UnitOfWork.SaveFundEntitiesAsync(request, cancellationToken);
            return saveResult;

        }
    }
}
