using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.Entities
{
    public class Fund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FundId { get; set; }
        public string FundName { get; set; }
        public int FundTypeId { get; set; }
        public int EngagementId { get; set; }
        public  DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
    }
}
