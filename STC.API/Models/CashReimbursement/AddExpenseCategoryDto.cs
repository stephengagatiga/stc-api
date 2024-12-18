using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class AddExpenseCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
