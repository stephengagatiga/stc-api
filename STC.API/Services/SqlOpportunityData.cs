using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.ComponentEntity;
using STC.API.Entities.OpportunityEntity;
using Microsoft.EntityFrameworkCore;

using STC.API.Models.Opportunity;

namespace STC.API.Services
{
    public class SqlOpportunityData : IOpportunityData
    {

        private STCDbContext _context;

        public SqlOpportunityData(STCDbContext context)
        {
            _context = context;
        }

        public Opportunity AddOpportunity(NewOpportunityDto newOpportunityDto)
        {
            var opportunity = new Opportunity() {
                AccountId = newOpportunityDto.AccountId,
                BigDealCode = newOpportunityDto.BigDealCode,
                Created = DateTime.Parse(newOpportunityDto.Created),
                CreatedById = newOpportunityDto.CreatedById,
                Modified = DateTime.Parse(newOpportunityDto.Modified),
                ModifiedById = newOpportunityDto.ModifiedById,
                Status = newOpportunityDto.Status
            };
            opportunity.Components = new List<Component>();

            var opp =_context.Opportunities.Add(opportunity);
            opp.State = Microsoft.EntityFrameworkCore.EntityState.Added;

            foreach (var component in newOpportunityDto.Components)
            {
                var c = new Component()
                {
                    OpportunityId = opportunity.Id,
                    Description = component.Description,
                    CategoryId = component.CategoryId,
                    ComponentTypeId = component.ComponentTypeId,
                    AccountExecutiveId = component.AccountExecutiveId,
                    SolutionsArchitectId = component.SolutionsArchitectId,
                    TargetCloseDate = DateTime.Parse(component.TargetCloseDate),
                    StageId = component.StageId,
                    ProductId = component.ProductId,
                    Qty = component.Qty,
                    PricePerUnit = component.PricePerUnit,
                    CostPerUnit = component.CostPerUnit,
                    Poc = component.Poc,
                    ValidityDate = DateTime.Parse(component.ValidityDate),
                    Status = component.Status,
                    Remarks = component.Remarks,
                    Created = DateTime.Parse(component.Created),
                    CreatedById = component.CreatedById,
                    Modified = DateTime.Parse(component.Modified),
                    ModifiedById = component.ModifiedById,
                    VersionNumber = 1
                };
  
                opportunity.Components.Add(c);
            }

            _context.SaveChanges();

            return opportunity;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.Where(c => c.Active == true).ToList();
        }

        public Component GetComponent(int componentId)
        {
            return _context.Components.FirstOrDefault(c => c.Id == componentId);
        }

        public ICollection<ComponentType> GetComponentTypes()
        {
            return _context.ComponentTypes.Where(c => c.Active == true).ToList();
        }

        public ICollection<Opportunity> GetOpportunities()
        {
            var opportunities = _context.Opportunities
                                        .Include(o => o.Account)
                                        .ThenInclude(a => a.Industry)
                                        .Include(o => o.Components)
                                        .ToList();
                                        
            return opportunities;
        }

        public ICollection<Stage> GetStages()
        {
            return _context.Stages.Where(s => s.Active == true).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Component UpdateComponent(EditComponentDto editComponent)
        {
            Opportunity opportunity = null;
            if (editComponent.BigDealCode != "")
            {
                opportunity = _context.Opportunities.FirstOrDefault(c => c.Id == editComponent.OpportunityId);
                if (opportunity == null)
                {
                    return null;
                }
                opportunity.BigDealCode = editComponent.BigDealCode;
                opportunity.Modified = DateTime.Now;
                opportunity.ModifiedById = editComponent.ModifiedById;

                _context.Opportunities.Update(opportunity);
                _context.Entry(opportunity).State = EntityState.Modified;

            }

            var component = _context.Components.FirstOrDefault(c => c.Id == editComponent.Id);
            if (component == null)
            {
                return null;
            }

            component.Description = editComponent.Description;
            component.CategoryId = editComponent.CategoryId;
            component.ComponentTypeId = editComponent.ComponentTypeId;
            component.AccountExecutiveId = editComponent.AccountExecutiveId;
            component.SolutionsArchitectId = editComponent.SolutionsArchitectId;
            component.TargetCloseDate = DateTime.Parse(editComponent.TargetCloseDate);
            component.StageId = editComponent.StageId;
            component.ProductId = editComponent.ProductId;
            component.Qty = editComponent.Qty;
            component.PricePerUnit = editComponent.PricePerUnit;
            component.CostPerUnit = editComponent.CostPerUnit;
            component.Poc = editComponent.Poc;
            component.ValidityDate = DateTime.Parse(editComponent.ValidityDate);
            component.Status = editComponent.Status;
            component.Remarks = editComponent.Remarks;
            component.Modified = DateTime.Now;
            component.ModifiedById = editComponent.ModifiedById;

            _context.Components.Update(component);
            _context.Entry(component).State = EntityState.Modified;

            _context.SaveChanges();

            return component;
        }
    }
}
