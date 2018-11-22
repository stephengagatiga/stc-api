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
        [HttpPost("isvaluematchregex")]
        public async Task<string> CheckIfValueIsMatchInRegex()
        {



            var tmp = await Request.GetRawBodyStringAsync();
            Console.WriteLine(tmp);
            return tmp;
        }

        [HttpPost("valuematchinregex")]
        public IActionResult CheckValueInMatchInRegex([FromBody] SubjectRegex regex)
        {
            var sb = new StringBuilder(regex.Regex);
            sb.Replace("\\", @"\");
/*            string unescapedString = Regex.Unescape(regex.Regex)*/;
            Console.WriteLine(sb.ToString());
            Match match = Regex.Match(regex.Subject, @sb.ToString(), RegexOptions.IgnorePatternWhitespace);
            Match match2 = Regex.Match(regex.Subject, @sb.ToString(), RegexOptions.IgnoreCase);

            if (match.Success || match2.Success)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }

    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            using (StreamReader reader = new StreamReader(request.Body, encoding))
                return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                await request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
