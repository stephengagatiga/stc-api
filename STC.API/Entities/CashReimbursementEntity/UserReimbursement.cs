using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.CashReimbursementEntity
{
    public class UserReimbursement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReimburseeId { get; set; }
        [ForeignKey("ReimburseeId")]
        public Reimbursee Reimbursee { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        public int? ProcessById { get; set; }
        [ForeignKey("ProcessById")]
        public User ProcessBy { get; set; }
        public DateTime? ProcessOn { get; set; }
        public string ReferenceNumber { get; set; }
        [Required]
        public DateTime ReimbursementDate { get; set; }
        [Required]
        public ReimbursementStatus ReimbursementStatus { get; set; }
        public ICollection<UserExpense> UserExpenses { get; set; }
    }
}
