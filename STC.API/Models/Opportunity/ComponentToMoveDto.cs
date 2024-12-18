using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class ComponentToMoveDto
    {
        public int Id { get; set; }
        public int NewOpportunityId { get; set; }
    }
}
