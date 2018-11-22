using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using STC.API.Models.Ticket;
using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;

namespace STC.API.Services
{
    public class Utils : IUtils
    {
        public string GeneratePassword()
        {
            var opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public List<EmailMessageDto> GetTicketEmails(int ticketId)
        {
            List<EmailMessageDto> msgs = new List<EmailMessageDto>();

            using (var client = new ImapClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("imap-mail.outlook.com", 993, true);
                client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");


                var sentItems = client.GetFolder("Sent Items");
                sentItems.Open(FolderAccess.ReadOnly);

                foreach (var uid in sentItems.Search(SearchQuery.SubjectContains($"[ID:{ticketId}]")))
                {
                    var message = sentItems.GetMessage(uid);
                    var headerFrom = "";
                    var headerFromEmail = "";
                    var helpdeskFrom = message.Headers.FirstOrDefault(h => h.Field == "X-HelpDesk-From");

                    if (helpdeskFrom != null)
                    {
                        headerFrom = helpdeskFrom.Value;
                        headerFromEmail = message.Headers.FirstOrDefault(h => h.Field == "X-HelpDesk-FromEmail").Value;
                    }

                    msgs.Add(new EmailMessageDto
                    {
                        Message = message.GetTextBody(MimeKit.Text.TextFormat.Plain),
                        Date = message.Date,
                        HeaderFrom = headerFrom,
                        HeaderFromEmail = headerFromEmail
                    });
                }

                var inbox = client.GetFolder("Inbox");
                inbox.Open(FolderAccess.ReadOnly);

                foreach (var uid in inbox.Search(SearchQuery.SubjectContains($"[ID:{ticketId}]")))
                {
                    var message = inbox.GetMessage(uid);
                    var headerFrom = "";
                    var headerFromEmail = "";
                    var helpdeskFrom = message.Headers.FirstOrDefault(h => h.Field == "X-HelpDesk-From");

                    if (helpdeskFrom != null)
                    {
                        headerFrom = helpdeskFrom.Value;
                        headerFromEmail = message.Headers.FirstOrDefault(h => h.Field == "X-HelpDesk-FromEmail").Value;
                    }

                    msgs.Add(new EmailMessageDto
                    {
                        Message = message.GetTextBody(MimeKit.Text.TextFormat.Plain),
                        Date = message.Date,
                        HeaderFrom = headerFrom,
                        HeaderFromEmail = headerFromEmail
                    });
                }


                client.Disconnect(true);
            }

            return msgs;
        }

        public async Task SendClientAccountCredentialAsync(string name, string email, string password)
        {
            try
            {
                //from
                string fromAddress = "stchelpdesk@shellsoft.com.ph";
                string fromAddressTitle = "Shellsoft HelpDesk";

                string toAddress = email;
                string ToAdressTitle = name;
                string Subject = "Your Credential in Shellsoft HelpDesk";
                string BodyContent = $"<b>Hi {name}!</b><br/>Username: {email}<br/>Password: {password}";

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(fromAddressTitle, fromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, toAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent

                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    await client.SendAsync(mimeMessage);
                    client.Disconnect(true);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Send Email Message Failed!");
                throw e;
            }
        }

        public async Task SendReply(string fromAddressTitle, string[] toAddress, string subject, string body, string userEmail)
        {
            try
            {
                
                //string toAddress = "phen@shellsoft.com.ph";
                //string toAdressTitle = "phen";

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC HelpDesk", "stchelpdesk@shellsoft.com.ph"));

                foreach(var to in toAddress)
                {
                    message.To.Add(new MailboxAddress(to));
                }

                message.Subject = subject;
                message.Headers.Add("X-HelpDesk-From", fromAddressTitle);
                message.Headers.Add("X-HelpDesk-FromEmail", userEmail);

                var builder = new BodyBuilder();


                // Set the plain-text version of the message text
                builder.TextBody = body;

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Send Email Message Failed!");
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
