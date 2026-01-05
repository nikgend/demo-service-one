using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Scoping.Domain.Common;

namespace Services.Scoping.Domain.AggregateRoots.EngagementAggregate
{
    public class EngagementDetails : IAggregateRoot
    {
        public int EngagementId { get; set; }
        public string EngagementName { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int EngagementTypeId { get; set; }
        public string EngagementType { get; set; }
    }
}
