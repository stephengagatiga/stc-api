using Microsoft.AspNetCore.Mvc;
using STC.API.Models.Opportunity;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Controllers
{

    [Route("opportunity")]
    public class OpportunityController : Controller
    {
        private IOpportunityData _opportunityData;

        public OpportunityController(IOpportunityData opportunityData)
        {
            _opportunityData = opportunityData;
        }

        [HttpPost]
        public IActionResult AddOpportunity([FromBody] NewOpportunityDto newOpportunityDto)
        {

            if (ModelState.IsValid)
            {
                if (newOpportunityDto.Components.Count == 0)
                {
                    return BadRequest();
                }
                var opp = _opportunityData.AddOpportunity(newOpportunityDto);
                return Ok(opp);
            }


            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetOpportunities()
        {
            var opportunities = _opportunityData.GetOpportunities();
            return Ok(opportunities);
        }

        [HttpGet("category")]
        public IActionResult GetCategories()
        {
            return Ok(_opportunityData.GetCategories());
        }

        [HttpGet("componenttype")]
        public IActionResult GetComponentTypes()
        {
            return Ok(_opportunityData.GetComponentTypes());
        }

        [HttpGet("stage")]
        public IActionResult GetStages()
        {
            return Ok(_opportunityData.GetStages());
        }

        [HttpPost("components")]
        public IActionResult EditComponent([FromBody] EditComponentDto editComponentDto)
        {
            if (ModelState.IsValid)
            {
                var updatedComponent = _opportunityData.UpdateComponent(editComponentDto);
                if (updatedComponent == null)
                {
                    return NotFound();
                }

                return Ok(updatedComponent);
            }

            return BadRequest();
        }
    }
}
