using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Product
{
    public class ProductEditDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
