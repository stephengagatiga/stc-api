using STC.API.Entities.OpportunityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IOpportunityData
    {
        Opportunity AddOpportunity();
    }
}
