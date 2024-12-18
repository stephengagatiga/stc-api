using STC.API.Entities.CashReimbursementEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.CashReimbursement
{
    public class AddUserReimbursement
    {
        [Required]
        public int ReimburseeId { get; set; }
        [Required]
        public int CreatedById { get; set; }
        [Required]
        public DateTime ReimbursementDate { get; set; }
        [Required]
        public ICollection<UserReimbursmentExpense> Expenses { get; set; }
    }

    public class UserReimbursmentExpense
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ExpenseId { get; set; }
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}
