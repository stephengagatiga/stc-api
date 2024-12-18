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
        NewOpportunitySuccessDto AddOpportunity(NewOpportunityDto newOpportunityDto);
        ICollection<Opportunity> GetOpportunities();
        Opportunity GetOpportunity(int opportunityId);
        ICollection<Category> GetCategories();
        ICollection<ComponentType> GetComponentTypes();
        ICollection<Stage> GetStages();
        Component GetComponent(int componentId);
        EditComponentSuccessDto UpdateComponent(EditComponentDto editComponent);
        AddComponentResultDto AddComponent(AddComponentDto addComponentDto, Opportunity opportunity);
        bool SaveMoveComponents(MoveComponentsDto moveComponentsDto);
        ICollection<FilteredComponentDto> GetComponentsByEmail(string email);
    }
}
