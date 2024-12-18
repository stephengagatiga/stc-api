using STC.API.Entities.CashReimbursement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.CashReimbursement
{
    public class EditUserReimbursement
    {
            [Required]
            public int ReimburseeId { get; set; }
            [Required]
            public int Id { get; set; }
            [Required]
            public DateTime ReimbursementDate { get; set; }
            [Required]
            public ICollection<UserReimbursmentExpense> Expenses { get; set; }
    }
}
