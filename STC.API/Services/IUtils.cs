using STC.API.Models.Ticket;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IUtils
    {
        string GeneratePassword();
        Task SendClientAccountCredentialAsync(string name, string email, string password);
        Task SendReply(string fromAddressTitle, string[] toAddress, string subject, string body, string userEmail);
        List<EmailMessageDto> GetTicketEmails(int ticketId);
    }
}
