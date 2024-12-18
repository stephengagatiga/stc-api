using STC.API.Entities.POEntity;
using STC.API.Entities.UserEntity;
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
        string GetServer();
        string GetApprover();
        string GetSharePointSite();

        string GeneratePassword();
        Task SendClientAccountCredentialAsync(string name, string email, string password);
        Task SendReply(string fromAddressTitle, string[] toAddress, string subject, string body, string userEmail);
        void SendPipelineNotif(ICollection<User> toAddress, ICollection<User> ccAddress, string subject, string body);
        void SendPipelineNotif(string[] to, string[] cc, string subject, string body);
        void SendPOApproval(POPending pOPending);
        void TestSend();
        List<EmailMessageDto> GetTicketEmails(int ticketId);
        void SendPORejectNotification(POGuidStatus pOGuidStatus);
        void SendPOToDistirubtionList(POGuidStatus pOGuidStatus, ICollection<POGuidStatusAttachment> attachments, POStatus pOStatus);

    }
}
