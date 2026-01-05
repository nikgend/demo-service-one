using MediatR;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.Contracts.Engagement
{
    [DataContract]
    public class CreateEngagementRequest : IRequest<EngagementNameCodeDuplicateCheck>
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int RegionId { get; set; }
        public int EngagementTypeId { get; set; }
    }
}
