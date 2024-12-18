using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.ComponentEntity;
using STC.API.Entities.OpportunityEntity;
using Microsoft.EntityFrameworkCore;

using STC.API.Models.Opportunity;
using STC.API.Entities.RequestEntity;
using Newtonsoft.Json;

namespace STC.API.Services
{
    public class SqlOpportunityData : IOpportunityData
    {

        private STCDbContext _context;

        public SqlOpportunityData(STCDbContext context)
        {
            _context = context;
        }

        public AddComponentResultDto AddComponent(AddComponentDto addComponentDto, Opportunity opportunity)
        {
            Guid requestId = Guid.NewGuid();
            Guid opportunityRequestId = Guid.NewGuid();

            DateTime now = DateTime.Now;

            if (addComponentDto.BigDealCode != "")
            {
                opportunity.BigDealCode = addComponentDto.BigDealCode;
            }


            if (opportunity.Status != Status.ForApproval)
            {
                opportunity.Status = Status.ForApproval;
                opportunity.RequestId = opportunityRequestId;

                Request opportunityRequest = new Request()
                {
                    Id = opportunityRequestId,
                    CreatedOn = now,
                    RequestAction = RequestAction.PENDING,
                    RequestSubject = RequestSubject.OPPORTUNITY,
                    RequestType = RequestType.ADD
                };

                _context.Request.Add(opportunityRequest);
                _context.Entry(opportunityRequest).State = EntityState.Added;
            }

            opportunity.Modified = DateTime.Now;
            opportunity.ModifiedById = addComponentDto.ModifiedById;


            _context.Opportunities.Update(opportunity);
            _context.Entry(opportunity).State = EntityState.Modified;

            var component = new Component() {
                AccountExecutiveId = addComponentDto.AccountExecutiveId,
                CategoryId = addComponentDto.CategoryId,
                ComponentTypeId = addComponentDto.ComponentTypeId,
                CostPerUnit = addComponentDto.CostPerUnit,
                Created = now,
                CreatedById = addComponentDto.CreatedById,
                Description = addComponentDto.Description,
                Modified = now,
                ModifiedById = addComponentDto.ModifiedById,
                OpportunityId = addComponentDto.OpportunityId,
                Poc = addComponentDto.Poc,
                PricePerUnit = addComponentDto.PricePerUnit,
                ProductId = addComponentDto.ProductId,
                Qty = addComponentDto.Qty,
                Remarks = addComponentDto.Remarks,
                SolutionsArchitectId = addComponentDto.SolutionsArchitectId,
                StageId = addComponentDto.StageId,
                Status = addComponentDto.Status,
                RequestId = requestId,
                VersionNumber = 1
            };

            if (addComponentDto.TargetCloseDate != null)
            {
                component.TargetCloseDate = DateTime.Parse(addComponentDto.TargetCloseDate);
            }

            if (addComponentDto.ValidityDate != null)
            {
                component.ValidityDate = DateTime.Parse(addComponentDto.ValidityDate);
            }

            Request request = new Request()
            {
                Id = requestId,
                CreatedOn = now,
                RequestAction = RequestAction.PENDING,
                RequestSubject = RequestSubject.COMPONENT,
                RequestType = RequestType.ADD
            };

     

            _context.Request.Add(request);
            _context.Entry(request).State = EntityState.Added;



            _context.Components.Add(component);
            _context.Entry(component).State = EntityState.Added;

            _context.SaveChanges();

            return new AddComponentResultDto() { Component = component, Opportunity = opportunity };
        }

        public NewOpportunitySuccessDto AddOpportunity(NewOpportunityDto newOpportunityDto)
        {
 
            Guid tokenId = Guid.NewGuid();

            DateTime Now = DateTime.Now;

            var opportunity = new Opportunity() {
                AccountId = newOpportunityDto.AccountId,
                BigDealCode = newOpportunityDto.BigDealCode,
                Created = Now,
                CreatedById = newOpportunityDto.CreatedById,
                Modified = Now,
                ModifiedById = newOpportunityDto.ModifiedById,
                Status = newOpportunityDto.Status,
                RequestId = tokenId,
            };

            _context.Opportunities.Add(opportunity);
            _context.Entry(opportunity).State = EntityState.Added;

            Request opportunityToken = new Request() {
                    Id = tokenId,
                    RequestSubject = RequestSubject.OPPORTUNITY,
                    RequestAction = RequestAction.PENDING,
                    RequestType = RequestType.ADD,
                    CreatedOn = Now
            };

            _context.Request.Add(opportunityToken);
            _context.Entry(opportunityToken).State = EntityState.Added;

            opportunity.Components = new List<Component>();

            foreach (var component in newOpportunityDto.Components)
            {
                var componentTokenId = Guid.NewGuid();

                var c = new Component()
                {
                    OpportunityId = opportunity.Id,
                    Description = component.Description,
                    CategoryId = component.CategoryId,
                    ComponentTypeId = component.ComponentTypeId,
                    AccountExecutiveId = component.AccountExecutiveId,
                    SolutionsArchitectId = component.SolutionsArchitectId,
                    StageId = component.StageId,
                    ProductId = component.ProductId,
                    Qty = component.Qty,
                    PricePerUnit = component.PricePerUnit,
                    CostPerUnit = component.CostPerUnit,
                    Poc = component.Poc,
                    Status = component.Status,
                    Remarks = component.Remarks,
                    Created = Now,
                    CreatedById = component.CreatedById,
                    Modified = Now,
                    ModifiedById = component.ModifiedById,
                    VersionNumber = 1,
                    RequestId = componentTokenId,
                    ComponentVersionId = null
                };

                if (component.TargetCloseDate != null)
                {
                    c.TargetCloseDate = DateTime.Parse(component.TargetCloseDate);
                }

                if (component.ValidityDate != null)
                {
                    c.ValidityDate = DateTime.Parse(component.ValidityDate);
                }
  
                opportunity.Components.Add(c);

                Request componentToken = new Request()
                {
                    Id = componentTokenId,
                    RequestSubject = RequestSubject.COMPONENT,
                    RequestAction = RequestAction.PENDING,
                    RequestType = RequestType.ADD,
                    CreatedOn = Now
                };

                _context.Request.Add(componentToken);
                _context.Entry(componentToken).State = EntityState.Added;

            }

            _context.SaveChanges();

            return new NewOpportunitySuccessDto { Opportunity = opportunity, Token = opportunityToken };
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.Where(c => c.Active == true).ToList();
        }

        public Component GetComponent(int componentId)
        {
            return _context.Components.FirstOrDefault(c => c.Id == componentId);
        }

        public ICollection<FilteredComponentDto> GetComponentsByEmail(string email)
        {
            var result = _context.Components
                .Include(c => c.Opportunity)
                .ThenInclude(o => o.Account)
                .Include(c => c.SolutionsArchitect)
                .Include(c => c.Stage)
                .Include(c => c.AccountExecutive)
                .Include(c => c.ComponentType)
                .Include(c => c.Description)
                .Include(c => c.Product)
                .Where(c => c.SolutionsArchitect.Email == email)
                .Select(c => new FilteredComponentDto
                { 
                   Id = c.Id,
                   AccountExecutive = c.AccountExecutive,
                   Account = c.Opportunity.Account,
                   ComponentType = c.ComponentType,
                   CostPerUnit = c.CostPerUnit,
                   Description = c.Description,
                   Modified = c.Modified,
                   Opportunity = c.Opportunity,
                   PricePerUnit = c.PricePerUnit,
                   Product = c.Product,
                   Qty = c.Qty,
                   Stage = c.Stage,
                   Status = c.Status,
                   Remarks = c.Remarks
                })
                .ToList();

            return result;



            throw new NotImplementedException();
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

        public Opportunity GetOpportunity(int opportunityId)
        {
            return _context.Opportunities.FirstOrDefault(o => o.Id == opportunityId);
        }

        public ICollection<Stage> GetStages()
        {
            return _context.Stages.Where(s => s.Active == true).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool SaveMoveComponents(MoveComponentsDto moveComponentsDto)
        {
            var opportunity1 = _context.Opportunities.FirstOrDefault(o => o.Id == moveComponentsDto.FirstOpportunityId);
            if (opportunity1 == null)
            {
                return false;
            }

            Opportunity opportunity2 = null;

            //Negative -1 means the opportunity doesnt exist
            if (moveComponentsDto.SecondOpportunityId == -1)
            {
                opportunity2 = new Opportunity()
                {
                    AccountId = opportunity1.AccountId,
                    BigDealCode = moveComponentsDto.SecondOpportunityIdBigDealCode,
                    Created = DateTime.Now,
                    CreatedById = moveComponentsDto.ModifiedById,
                    Status = Status.Open,
                    Modified = DateTime.Now,
                    ModifiedById = moveComponentsDto.ModifiedById
                };

                _context.Opportunities.Add(opportunity2);
                _context.Entry(opportunity2).State = EntityState.Added;

            } else
            {
                opportunity2 = _context.Opportunities.FirstOrDefault(o => o.Id == moveComponentsDto.SecondOpportunityId);

                if (opportunity2 == null)
                {
                    return false;
                }

                opportunity2.BigDealCode = moveComponentsDto.SecondOpportunityIdBigDealCode;
                opportunity2.Modified = DateTime.Now;
                opportunity2.ModifiedById = moveComponentsDto.ModifiedById;

                _context.Opportunities.Update(opportunity2);
                _context.Entry(opportunity2).State = EntityState.Modified;
            }

            opportunity1.BigDealCode = moveComponentsDto.FirstOpportunityIdBigDealCode;
            opportunity1.Modified = DateTime.Now;
            opportunity1.ModifiedById = moveComponentsDto.ModifiedById;

            var hasError = false;
            foreach (var c in moveComponentsDto.Components)
            {
                var component = _context.Components.FirstOrDefault(com => com.Id == c.Id);
                if (component == null)
                {
                    hasError = true;
                } else
                {
                    if (moveComponentsDto.SecondOpportunityId == -1)
                    {
                        component.OpportunityId = opportunity2.Id;
                    } else
                    {
                        component.OpportunityId = c.NewOpportunityId;
                    }
                    component.Modified = DateTime.Now;
                    component.ModifiedById = moveComponentsDto.ModifiedById;
                    _context.Components.Update(component);
                    _context.Entry(component).State = EntityState.Modified;
                }  
            }

            if (hasError)
            {
                return false;
            }

            _context.Opportunities.Update(opportunity1);
            _context.Entry(opportunity1).State = EntityState.Modified;

            _context.SaveChanges();
            return true;
        }

        public EditComponentSuccessDto UpdateComponent(EditComponentDto editComponent)
        {
            Guid requestId = Guid.NewGuid();
            DateTime now = DateTime.Now;
            Opportunity opportunity = _context.Opportunities.FirstOrDefault(c => c.Id == editComponent.OpportunityId);

            if (opportunity == null)
            {
                return null;
            }

            if (editComponent.BigDealCode != "")
            {
                opportunity.BigDealCode = editComponent.BigDealCode;
            }

            var component = _context.Components.FirstOrDefault(c => c.Id == editComponent.Id);
            if (component == null)
            {
                return null;
            }

            var isComponentNeedForApproval = false;

            if (component.CategoryId != editComponent.CategoryId || component.ComponentTypeId != editComponent.ComponentTypeId || component.AccountExecutiveId != editComponent.AccountExecutiveId || component.SolutionsArchitectId != editComponent.SolutionsArchitectId || !component.TargetCloseDate.Equals(DateTime.Parse(editComponent.TargetCloseDate)) || component.StageId != editComponent.StageId || component.ProductId != editComponent.ProductId || component.Qty != editComponent.Qty || component.PricePerUnit != editComponent.PricePerUnit || component.CostPerUnit != editComponent.CostPerUnit || component.Poc != editComponent.Poc || !component.ValidityDate.Equals(DateTime.Parse(editComponent.ValidityDate)) || component.ModifiedById != editComponent.ModifiedById) {
                isComponentNeedForApproval = true;
            }

            var currentComponentVersion = component.VersionNumber;

            var componentVersion = new ComponentVersion()
            {
                ComponentId = component.Id,
                Added = now,
                OpportunityId = component.OpportunityId,
                Description = component.Description,
                CategoryId = component.CategoryId,
                AccountExecutiveId = component.AccountExecutiveId,
                SolutionsArchitectId = component.SolutionsArchitectId,
                StageId = component.StageId,
                ProductId = component.ProductId,
                Qty = component.Qty,
                PricePerUnit = component.PricePerUnit,
                CostPerUnit = component.CostPerUnit,
                POC = component.Poc,
                Status = component.Status,
                Remarks = component.Remarks,
                Modified = (DateTime)component.Modified,
                ModifiedById = component.ModifiedById,
                RequestId = component.RequestId,
                VersionNumber = currentComponentVersion,
             };

            if (component.TargetCloseDate != null)
            {
                componentVersion.TargetCloseMonth = (DateTime)component.TargetCloseDate;
            }

            if (component.ValidityDate != null)
            {
                componentVersion.Validity = (DateTime)component.ValidityDate;
            }

            if (component.ComponentTypeId != null)
            {
                componentVersion.ComponentTypeId = (int)component.ComponentTypeId;
            }

            if (isComponentNeedForApproval)
            {
                Request newRequest = new Request()
                {
                    Id = requestId,
                    CreatedOn = now,
                    RequestAction = RequestAction.PENDING,
                    RequestSubject = RequestSubject.COMPONENT,
                    RequestType = RequestType.EDIT
                };

                if (opportunity.Status != Status.ForApproval)
                {

                    Guid newOpportunityRequestId = Guid.NewGuid();
                    Request newOpportunityRequest = new Request()
                    {
                        Id = newOpportunityRequestId,
                        CreatedOn = now,
                        RequestAction = RequestAction.PENDING,
                        RequestSubject = RequestSubject.OPPORTUNITY,
                        RequestType = RequestType.EDIT
                    };

                    opportunity.Status = Status.ForApproval;
                    opportunity.RequestId = newOpportunityRequestId;

                    _context.Request.Add(newOpportunityRequest);
                    _context.Entry(newOpportunityRequest).State = EntityState.Added;
                }

                _context.Request.Add(newRequest);
                _context.Entry(newRequest).State = EntityState.Added;

            }

            opportunity.Modified = DateTime.Now;
            opportunity.ModifiedById = editComponent.ModifiedById;

            _context.Opportunities.Update(opportunity);
            _context.Entry(opportunity).State = EntityState.Modified;

 


            _context.ComponentVersions.Add(componentVersion);
            _context.Entry(componentVersion).State = EntityState.Added;

            //We need to save changes so we can get the componentVersion.Id
            _context.SaveChanges();

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
            component.Status = isComponentNeedForApproval ? Status.ForApproval : component.Status;
            component.Remarks = editComponent.Remarks;
            component.Modified = DateTime.Now;
            component.ModifiedById = editComponent.ModifiedById;
            component.VersionNumber = currentComponentVersion + 1;
            component.RequestId = isComponentNeedForApproval ? requestId : component.RequestId;
            component.ComponentVersionId = componentVersion.Id;

            _context.Components.Update(component);
            _context.Entry(component).State = EntityState.Modified;

            _context.SaveChanges();



            return new EditComponentSuccessDto() { Component = component, Opportunity = opportunity };

        }
    }
}
