using STC.API.Entities.AccountEntity;
using STC.API.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.TicketEntity
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Body { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public int RequesterId { get; set; }
        [ForeignKey("RequesterId")]
        public AccountContact Requester { get; set; }

        public int? AssigneeId { get; set; }
        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }

        public TicketStatus Status { get; set; }
        public TicketType Type { get; set; }
        public TicketPriority Priority { get; set; }

        [DataType(DataType.Text)]
        public string Products { get; set; }

        public ICollection<TicketProcedure> Procedures { get; set; }
        public ICollection<TicketHistory> Histories { get; set; }

        [DataType(DataType.Text)]
        public string RootCause { get; set; }

        [DataType(DataType.Text)]
        public string Resolution { get; set; }

        public DateTime? ClosedOn { get; set; }
        public DateTime CreatedOn { get; set;  }

    }
}
