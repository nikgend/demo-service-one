using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Domain.Entities.engagement
{
    public class Engagement 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EngagementId { get; set; }

        public string EngagementName { get; set; } = string.Empty;

        public int RegionId { get; set; }
        public int EngagementTypeId { get; set; }
    }
}
