using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class EditReimburseeDto
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BankAccountNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
