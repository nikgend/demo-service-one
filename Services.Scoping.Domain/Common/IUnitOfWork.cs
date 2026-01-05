using Services.Scoping.Domain.Contracts;
using Services.Scoping.Domain.Contracts.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveEngagementEntitiesAsync(CreateEngagementRequest request, CancellationToken cancellationToken);
        Task<bool> UpdateEngagementEntitiesAsync(UpdateEngagementRequest request, CancellationToken cancellationToken = default);
        Task<int> SaveFundEntitiesAsync(CreateFundRequest createFundRequest, CancellationToken cancellationToken = default);
    }
}
