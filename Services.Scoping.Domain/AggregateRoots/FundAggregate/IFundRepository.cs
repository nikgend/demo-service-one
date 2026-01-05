using Services.Scoping.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.FundAggregate
{
    public interface IFundRepository : IRepository<AssetManagementAnalysisDetails>
    {
        Task<List<AssetManagementAnalysisDetails>> GetAllFund(int engagementId);
    }
}
