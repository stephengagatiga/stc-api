using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.ProductEntity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PrincipalId")]
        public Principal Principal { get; set; }
        public int PrincipalId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
