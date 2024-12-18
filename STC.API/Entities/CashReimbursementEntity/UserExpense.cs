using STC.API.Entities.CashReimbursementEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.CashReimbursementEntity
{
    public class UserExpense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserReimbursementId { get; set; }
        [ForeignKey("UserReimbursementId")]
        public UserReimbursement UserReimbursement { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ExepenseId { get; set; }
        [ForeignKey("ExepenseId")]
        public Expense Expense { get; set; }
        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}
