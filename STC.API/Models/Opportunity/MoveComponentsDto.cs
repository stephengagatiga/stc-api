using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class MoveComponentsDto
    {
        public int FirstOpportunityId { get; set; }
        public string FirstOpportunityIdBigDealCode { get; set; }
        public int SecondOpportunityId { get; set; }
        public string SecondOpportunityIdBigDealCode { get; set; }
        public ICollection<ComponentToMoveDto> Components { get; set; }
        public int ModifiedById { get; set; }
    }
}
