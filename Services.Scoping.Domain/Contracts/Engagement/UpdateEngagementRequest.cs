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
    public class UpdateEngagementRequest : IRequest<EngagementNameCodeDuplicateCheck>
    {
        [DataMember]
        public int EngagementId { get; set; }
        [DataMember]
        public string Name { get; set; }
        public int EngagementTypeId { get; set; }
    }
}
