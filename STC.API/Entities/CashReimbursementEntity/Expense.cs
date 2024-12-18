using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.CashReimbursementEntity
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ExpenseCategoryId { get; set; }
        [ForeignKey("ExpenseCategoryId")]
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
