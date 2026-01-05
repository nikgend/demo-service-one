using Services.Scoping.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.FundAggregate
{
    public class AssetManagementAnalysisDetails : IAggregateRoot
    {
        public int EngagementId { get; set; }
        public string FundName { get; set; }
        public int FundId { get; set; }
        public int FundTypeId { get; set; }
        public string FundType { get; set; }
        public string engagementName { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
    }
}
