using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class SaveBatchDto
    {
        [Required]
        public int BatchId { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public int RecordsCount { get; set; }
    }
}
