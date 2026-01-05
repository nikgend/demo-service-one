using Services.Scoping.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.EngagementAggregate
{
    public class EngagementNameCodeDuplicateCheck : IAggregateRoot
    {
        public string EngagementId { get; set; }
        public string Name { get; set; }
        public bool IsNameExist { get; set; }
        public bool ValidateNewEngagement { get; set; }
        public string message { get; set; }
    }
}
