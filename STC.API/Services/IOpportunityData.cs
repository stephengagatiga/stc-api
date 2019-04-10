using STC.API.Entities.ComponentEntity;
using STC.API.Entities.OpportunityEntity;
using STC.API.Models.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IOpportunityData
    {
        Opportunity AddOpportunity(NewOpportunityDto newOpportunityDto);
        ICollection<Opportunity> GetOpportunities();
        ICollection<Category> GetCategories();
        ICollection<ComponentType> GetComponentTypes();
        ICollection<Stage> GetStages();
        Component GetComponent(int componentId);
        Component UpdateComponent(EditComponentDto editComponent);
    }
}
