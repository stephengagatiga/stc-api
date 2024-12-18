using STC.API.Entities.ComponentEntity;
using STC.API.Entities.RequestEntity;
using STC.API.Entities.OpportunityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class AddComponentResultDto
    {
        public Component Component { get; set; }
        public Entities.OpportunityEntity.Opportunity Opportunity  { get; set; }
    }
}
