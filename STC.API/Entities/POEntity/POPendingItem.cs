using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.POEntity
{
    public class POPendingItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int POPendingId { get; set; }
        [ForeignKey("POPendingId")]
        public POPending POPending { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}
