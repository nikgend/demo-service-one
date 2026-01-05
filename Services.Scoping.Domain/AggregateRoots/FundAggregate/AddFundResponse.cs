using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.AggregateRoots.FundAggregate
{
    public class AddFundResponse 
    {
        public int FundId { get; set; }
        public string message { get; set; }
    }
}
