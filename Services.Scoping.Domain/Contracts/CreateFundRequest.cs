using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.Contracts
{
    [DataContract]
    public class CreateFundRequest : IRequest<int>
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EngagementId { get; set; }
       
        public int FundTypeId { get; set; }
        
        [DataMember]
        public DateTime PeriodStartDate { get; set; }
        [DataMember]
        public DateTime PeriodEndDate { get; set; }
       

    }
}
