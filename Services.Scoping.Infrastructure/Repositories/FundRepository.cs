using Microsoft.EntityFrameworkCore;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using Services.Scoping.Domain.Common;
using Services.Scoping.Domain.Entities;
using Services.Scoping.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Infrastructure.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly ScopingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public FundRepository(ScopingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<List<AssetManagementAnalysisDetails>> GetAllFund(int engagementId)
        {
            var analysisDetails = new List<AssetManagementAnalysisDetails>();

            var data = await (from fund in _context.Fund
                              join engagement in _context.Engagement on fund.EngagementId equals engagement.EngagementId
                              join fundType in _context.FundType on fund.FundTypeId equals fundType.FundTypeId
                              where fund.EngagementId == engagementId
                              select new AssetManagementAnalysisDetails
                              {
                                FundId = fund.FundId,
                                FundName = fund.FundName,
                                EngagementId = fund.EngagementId,
                                FundTypeId = fund.FundTypeId   ,
                                FundType = fundType.FundTypeName,
                                engagementName = engagement.EngagementName,
                                PeriodStartDate = fund.PeriodStartDate,
                                PeriodEndDate = fund.PeriodEndDate

                              }).ToListAsync();

            return await Task.FromResult(data);
        }
    }
}
    

