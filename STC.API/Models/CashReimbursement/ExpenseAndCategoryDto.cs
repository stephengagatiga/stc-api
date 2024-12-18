using STC.API.Entities.CashReimbursementEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class ExpenseAndCategoryDto
    {
        public ICollection<Expense> Expense { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategory { get; set; }
    }
}
