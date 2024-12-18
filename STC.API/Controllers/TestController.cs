using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("heroes")]
    public class TestController : Controller
    {

        IInMemoryData _inMemoryData;

        public TestController(IInMemoryData inMemoryData)
        {
            _inMemoryData = inMemoryData;
        }

        [AllowAnonymous]
        [HttpGet("chart.{format}"), FormatFilter]
        public IActionResult GetHeroesChart()
        {
            List<Hero> heroes = new List<Hero>();
            heroes.Add(new Hero()
            {
                HeroName = "All Might",
                CurrentRank = "Retired",
                PreviousRank = "1",
                Status = "Inactive"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Endeavor",
                CurrentRank = "1",
                PreviousRank = "2",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Hawks",
                CurrentRank = "2",
                PreviousRank = "3",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Best Jeanist",
                CurrentRank = "3",
                PreviousRank = "4",
                Status = "Inactive"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Edgeshot",
                CurrentRank = "4",
                PreviousRank = "5",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Mirko",
                CurrentRank = "5",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Crust",
                CurrentRank = "6",
                PreviousRank = "6",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Kamui Woods",
                CurrentRank = "7",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Wash",
                CurrentRank = "8",
                PreviousRank = "Unknown",
                Status = "Active"
            });


            heroes.Add(new Hero()
            {
                HeroName = "Yoroi Musha",
                CurrentRank = "9",
                PreviousRank = "8",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Ryukyu",
                CurrentRank = "10",
                PreviousRank = "9",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Gang Orca",
                CurrentRank = "12",
                PreviousRank = "10",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Shishido",
                CurrentRank = "13",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Mt. Lady",
                CurrentRank = "23",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Fat Gum",
                CurrentRank = "58",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Manual",
                CurrentRank = "222",
                PreviousRank = "Unknown",
                Status = "Active"
            });

            heroes.Add(new Hero()
            {
                HeroName = "Wild, Wild Pussycats",
                CurrentRank = "411",
                PreviousRank = "32",
                Status = "Active"
            });

            return Ok(heroes);
        }
    
        [AllowAnonymous]
        [HttpPost("obj")]
        public IActionResult SetObject([FromBody] Object obj)
        {
            _inMemoryData.SetObject(obj);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("obj")]
        public IActionResult GetObject()
        {
            var obj = _inMemoryData.GetObject();
            return Ok(obj);
        }
    }

    [DataContract]
    class Hero
    {
        [DataMember]
        public string HeroName { get; set; }
        [DataMember]
        public string CurrentRank { get; set; }
        [DataMember]
        public string PreviousRank { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
