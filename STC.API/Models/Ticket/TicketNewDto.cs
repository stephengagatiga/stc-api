using STC.API.Entities.TicketEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Ticket
{
    public class TicketNewDto
    {

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int RequesterId { get; set; }

        [Required]
        public int AssigneeId { get; set; }

        [Required]
        public TicketType Type { get; set; }

        [Required]
        public TicketPriority Priority { get; set; }

        [Required]
        public string Products { get; set; }
    }
}
