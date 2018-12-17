using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using Newtonsoft.Json.Linq;
using STC.API.Models.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("internal")]
    public class InternalController : Controller
    {
   
        [AllowAnonymous]
        [HttpPost("valuematchinregex")]
        public IActionResult CheckValueInMatchInRegex([FromBody] SubjectRegex regex)
        {

            Match match = Regex.Match(regex.Subject, $@"{regex.Regex}", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return Ok(true);
            }
            return Ok(false);


        }

        [AllowAnonymous]
        [HttpPost]
        [Route("test")]
        public async Task<string> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                Console.WriteLine(content);
                return content;
            }
        }
    }

}
