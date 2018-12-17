using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Utils
{
    public class ActiveState
    {
        [Required]
        public bool Active { get; set; }
    }
}
