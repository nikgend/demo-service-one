using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.AMRCloud.Services.Scoping.Domain.Entities
{
    public class Fund
    {
        public int Id { get; set; }
        public string? FundCode { get; set; }
        public string? FundName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
