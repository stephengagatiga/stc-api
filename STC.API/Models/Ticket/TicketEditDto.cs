using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Ticket
{
    public enum TicketEditStatus
    {
        Open = 0,
        Pending = 1,
        Canceled = 3,
        Solved = 4
    }

    public enum TicketEditPriority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4
    }

    public enum TicketEditType
    {
        Inquiry = 1,
        Incident = 2
    }

    public class TicketEditDto
    {
        [Required]
        public int AssigneeId { get; set; }

        [Required]
        public TicketEditStatus Status { get; set; }

        [Required]
        public TicketEditPriority Priority { get; set; }

        [Required]
        public TicketEditType Type { get; set; }

        [Required]
        public string Products { get; set; }
    }
}
