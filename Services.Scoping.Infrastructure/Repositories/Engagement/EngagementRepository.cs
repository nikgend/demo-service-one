using Microsoft.EntityFrameworkCore;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Common;
using Services.Scoping.Domain.Contracts.Engagement;
using Services.Scoping.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Infrastructure.Repositories.Engagement
{
    public class EngagementRepository : IEngagementRepository
    {
        
        private readonly ScopingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public EngagementRepository(ScopingDbContext context)
        {
            _context = context;
        }

        public async Task<List<EngagementDetails>> GetEngagementDetailsAsync()
        {
           
            var data = await (from engagement in _context.Engagement
                              join region in _context.Region on engagement.RegionId equals region.RegionId
                                join engagementType in _context.EngagementType on engagement.EngagementTypeId equals engagementType.EngagementTypeId
                              select new EngagementDetails
                              {
                                  EngagementId = engagement.EngagementId,
                                  EngagementName = engagement.EngagementName,
                                  RegionId = engagement.RegionId,
                                  EngagementTypeId = engagement.EngagementTypeId,
                                  EngagementType = engagementType.EngagementTypeName,
                                    RegionName = region.RegionName
                              }).ToListAsync();


            // Simulate async operation
            await Task.CompletedTask;
            return data;
        }

        public bool GetEngagementNameExistenceDetails(CreateEngagementRequest createrequest, UpdateEngagementRequest updateRequest)
        {

            bool isNameExist = false;
            if (createrequest != null)
                isNameExist = _context.Engagement.Any(e => e.EngagementName.ToLower() == createrequest.Name.ToLower());
            else if (updateRequest != null)
                isNameExist = _context.Engagement.Any(e => e.EngagementId != updateRequest.EngagementId && e.EngagementName.ToLower() == updateRequest.Name.ToLower());
            return isNameExist;
        }
    }

   
}
