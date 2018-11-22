using STC.API.Entities.TicketEntity;
using STC.API.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface ITicketData
    {
        Ticket AddTicket(TicketNewDto ticketNewDto);
        ICollection<Ticket> GetTicketsByAssignee(int assigneeId);
        ICollection<Ticket> GetAllTickets();
        Ticket GetTicket(int ticketId);
        Ticket GetTicketInfo(int ticketId);
        void UpdateTicket(Ticket ticket);
    }
}
