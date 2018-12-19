using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Internal
{
    public class UserInThisProductsDto
    {
        public string[] Products { get; set; }
        public string Role { get; set; }
    }
}
