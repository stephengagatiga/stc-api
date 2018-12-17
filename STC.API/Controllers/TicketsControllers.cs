using STC.API.Models.Ticket;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using STC.API.Entities.TicketEntity;
using System.IdentityModel.Tokens.Jwt;
using STC.API.Models.User;

namespace STC.API.Controllers
{
    [Route("tickets")]
    public class TicketsControllers : Controller
    {
        private ITicketData _ticketData;
        private IAccountData _accountData;
        private IProductData _productData;
        private IUserData _userData;
        private IUtils _utils;

        public TicketsControllers(ITicketData ticketData, IAccountData accountData,  IProductData productData, IUserData userData, IUtils utils)
        {
            _ticketData = ticketData;
            _accountData = accountData;
            _productData = productData;
            _userData = userData;
            _utils = utils;
        }

        [HttpPost]
        public IActionResult AddTicket([FromBody] TicketNewDto ticketNewDto)
        {
            if (ModelState.IsValid)
            {
                var account = _accountData.GetAccountById(ticketNewDto.AccountId);

                if (account == null)
                {
                    return StatusCode(404, "Account Not Found");
                }

                if (ticketNewDto.RequesterId != -1)
                {
                    var accountContact = _accountData.GetAccountContact(ticketNewDto.RequesterId);
                    if (accountContact == null)
                    {
                        return StatusCode(404, "Account Contact Not Found");
                    }
                }

                var ticket = _ticketData.AddTicket(ticketNewDto);
                if (ticket == null)
                {
                    return StatusCode(500);
                }
                return Ok(ticket);
            }
            return BadRequest();
        }

        [HttpGet("assignee/{userId}")]
        public IActionResult GetTicketsByAssignee(int userId)
        {
            var tickets = _ticketData.GetTicketsByAssignee(userId);
            return Ok(tickets);
        }

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            var tickets = _ticketData.GetAllTickets();
            return Ok(tickets);
        }

        [HttpGet("{ticketId}")]
        public IActionResult GetTicket(int ticketId)
        {
            var ticket = _ticketData.GetTicket(ticketId);
            if (ticket == null)
            {
                return StatusCode(404, "Ticket Not Found");
            }

            var products = ticket.Products.Split(",");

            return Ok(new
            {
                ticket.Id,
                ticket.Subject,
                ticket.Body,
                ticket.Account,
                ticket.Requester,
                ticket.Assignee,
                ticket.Status,
                ticket.Type,
                ticket.Priority,
                products,
                ticket.RootCause,
                ticket.Resolution,
                ticket.ClosedOn,
                ticket.CreatedOn
            });
        }

        [HttpPost("{ticketId}")]
        public IActionResult EditTicket([FromBody] TicketEditDto ticketEditDto, int ticketId)
        {
            if (ModelState.IsValid)
            {
                var ticket = _ticketData.GetTicket(ticketId);
                if (ticket == null)
                {
                    return NotFound();
                }

                ticket.AssigneeId = ticketEditDto.AssigneeId;
                ticket.Status = (TicketStatus)ticketEditDto.Status;
                ticket.Type = (TicketType)ticketEditDto.Type;
                ticket.Priority = (TicketPriority)ticketEditDto.Priority;
                ticket.Products = ticketEditDto.Products;

                _ticketData.UpdateTicket(ticket);

                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("test")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Test()
        {
            var accessToken = Request.Headers["Authorization"];
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(accessToken.ToString().Split(' ')[1]) as JwtSecurityToken;
            var name = tokenS.Claims.FirstOrDefault(t => t.Type == "name").Value;

            Console.WriteLine(name);

            return Ok();
        }

        [HttpPost("{ticketId}/reply")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SendReply([FromBody] SendReplyDto reply, int ticketId)
        {
            if (ModelState.IsValid)
            {

                var ticket = _ticketData.GetTicketInfo(ticketId);
                if (ticket == null)
                {
                    return NotFound();
                }
 
                var accessToken = Request.Headers["Authorization"];
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(accessToken.ToString().Split(' ')[1]) as JwtSecurityToken;
                var name = tokenS.Claims.FirstOrDefault(t => t.Type == "name").Value;
                var email = tokenS.Claims.FirstOrDefault(t => t.Type == "unique_name").Value;

                await _utils.SendReply(name, reply.To, $"[ID:{ticketId}] {ticket.Subject}", reply.Message, email);

                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("{ticketId}/emails")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetTicketEmails(int ticketId)
        {
            var msgs = _utils.GetTicketEmails(ticketId);
            return Ok(msgs);
        }

    }
}
