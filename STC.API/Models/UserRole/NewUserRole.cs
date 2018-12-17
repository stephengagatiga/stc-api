using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.UserRole
{
    public class NewUserRole
    {
        [Required]
        public string Name { get; set; }
    }
}
