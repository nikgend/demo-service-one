using Services.Scoping.Domain.Common;
using Services.Scoping.Domain.Contracts.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.EngagementAggregate
{
    public interface IEngagementRepository : IRepository<EngagementDetails>
    {
        Task<List<EngagementDetails>> GetEngagementDetailsAsync();
        bool GetEngagementNameExistenceDetails(CreateEngagementRequest createrequest, UpdateEngagementRequest updateRequest);
    }
}
