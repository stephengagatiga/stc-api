using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.TicketEntity;
using STC.API.Models.Ticket;
using Microsoft.EntityFrameworkCore;

namespace STC.API.Services
{
    public class SqlTicketData : ITicketData
    {
        private STCDbContext _context;

        public SqlTicketData(STCDbContext context)
        {
            _context = context;
        }

        public Ticket AddTicket(TicketNewDto newTicket)
        {
            try
            {
                Ticket ticket = new Ticket {
                    Subject = newTicket.Subject,
                    Body = newTicket.Body,
                    AccountId = newTicket.AccountId,
                    Type = newTicket.Type,
                    Products = newTicket.Products,
                    Status = newTicket.AssigneeId == -1 ? TicketStatus.Open : TicketStatus.Pending,
                    Priority = newTicket.Priority,
                    Procedures = new List<TicketProcedure>(),
                    Histories = new List<TicketHistory>(),
                    CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")),
                    RequesterId = newTicket.RequesterId,
                };

                // leave undefined if not set
                if (newTicket.AssigneeId != -1)
                {
                    ticket.AssigneeId = newTicket.AssigneeId;
                } 

                _context.Tickets.Add(ticket);
                _context.Entry(ticket).State = EntityState.Added;

                _context.SaveChanges();

                return ticket;

            } catch (SqlException ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        

        public ICollection<Ticket> GetAllTickets()
        {
            var tickets = _context.Tickets
                                    .Include(a => a.Account)
                                    .Include(ac => ac.Requester)
                                    .Include(asgn => asgn.Assignee)
                                    .OrderByDescending(t => t.Id ).ToList();
            return tickets;
        }

        public Ticket GetTicket(int ticketId)
        {
            var ticket = _context.Tickets
                            .Include(a => a.Account)
                            .Include(ac => ac.Requester)
                            .Include(asgn => asgn.Assignee)
                            .Include(pc => pc.Procedures)
                            .Include(h => h.Histories)
                            .FirstOrDefault(t => t.Id == ticketId);

            return ticket;
        }

        public Ticket GetTicketInfo(int ticketId)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == ticketId);
            return ticket;
        }

        public ICollection<Ticket> GetTicketsByAssignee(int assigneeId)
        {
            //var tickets = _context.Tickets
            //                .Where(t => t.AssigneeId == assigneeId)
            //                .OrderByDescending(o => o.CreatedOn)
            //                .Include(a => a.Account)
            //                .Include(u => u.Assignee)
            //                .ToList();

            return null;
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.Entry(ticket).State = EntityState.Modified;
            SaveChanges();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
