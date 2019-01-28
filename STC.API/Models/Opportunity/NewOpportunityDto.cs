using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class NewOpportunityDto
    {
        public int AccountId { get; set; }
        public string BigDealCode { get; set; }
        public int CreatedById { get; set; }
    }
}
