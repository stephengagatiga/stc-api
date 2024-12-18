using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class ProcessedDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ReferenceNumber { get; set; }
    }
}
