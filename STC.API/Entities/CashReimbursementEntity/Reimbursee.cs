using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.CashReimbursementEntity
{
    public class Reimbursee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BankAccountNumber { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
