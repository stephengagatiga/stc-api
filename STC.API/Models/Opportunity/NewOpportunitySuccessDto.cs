using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class NewOpportunitySuccessDto
    {
        public Entities.OpportunityEntity.Opportunity Opportunity { get; set; }
        public Entities.RequestEntity.Request Token { get; set; }
    }
}
