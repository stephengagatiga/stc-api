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
using System.Text;
using System.IO;
using STC.API.Entities.UserEntity;
using STC.API.Entities.POEntity;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Globalization;

namespace STC.API.Services
{
    public class Utils : IUtils
    {
        public const string SERVER = "https://stc-api.azurewebsites.net"; //https://stc-api.azurewebsites.net //https://localhost:5001
        public const string APPROVER = "ferdinand@shellsoft.com.ph"; //"ferdinand@shellsoft.com.ph";
        public const string PO_DISTRIBUTION_LIST_EMAIL = "phen@shellsoft.com.ph";
        public const string SHAREPOINT_SITE = "https://shellsoftph.sharepoint.com/sites/LOBA/SitePages/Opportunity.aspx"; //https://shellsoftph.sharepoint.com/sites/LOBA/SitePages/Opportunity.aspx

        private CultureInfo EN_US = new CultureInfo("en-US");

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

                    var emailMessage = "";
                    var emailMessageHtml = message.GetTextBody(MimeKit.Text.TextFormat.Html);
                    if (emailMessageHtml != null)
                    {
                        emailMessage = PopulateInlineImages(message, emailMessageHtml);
                    }
                    else
                    {
                        emailMessage = message.GetTextBody(MimeKit.Text.TextFormat.Text);
                    }

                    msgs.Add(new EmailMessageDto
                    {
                        Message = emailMessage,
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
                    } else
                    {
                        var headerFromSplit = message.Headers.FirstOrDefault(h => h.Field == "From").Value.Split(' ');
                        var tmpFrom = new StringBuilder();
                        for(var i = 0; i < headerFromSplit.Length-1; i++)
                        {
                            tmpFrom.Append(headerFromSplit[i]);
                        }
                        headerFrom = tmpFrom.ToString();
                        headerFromEmail = headerFromSplit[headerFromSplit.Length - 1];
                        headerFromEmail = headerFromEmail.Substring(1, headerFromEmail.Length - 2);
                    }

                    var emailMessage = "";
                    var emailMessageHtml = message.GetTextBody(MimeKit.Text.TextFormat.Html);
                    if (emailMessageHtml != null)
                    {
                        emailMessage = PopulateInlineImages(message, emailMessageHtml);
                    } else
                    {
                        emailMessage = message.GetTextBody(MimeKit.Text.TextFormat.Text);
                    }

                    msgs.Add(new EmailMessageDto
                    {
                        Message = emailMessage,
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
                builder.HtmlBody = body;

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

        public void SendPipelineNotif(string[] toAddress, string[] ccAddress, string subject, string body)
        {
            try
            {

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC Pipeline", "powerbi@shellsoft.com.ph"));

                foreach (var to in toAddress)
                {
                    message.To.Add(new MailboxAddress(to));
                }

                message.Subject = subject;


                var builder = new BodyBuilder();


                // Set the plain-text version of the message text
                builder.TextBody = body;
                builder.HtmlBody = body;

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("powerbi@shellsoft.com.ph", "*3-hO3Lr");
                    //client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        public void SendPipelineNotif(ICollection<User> toAddress, ICollection<User> ccAddress, string subject, string body)
        {
            try
            {

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC Pipeline", "powerbi@shellsoft.com.ph"));

                foreach (var to in toAddress)
                {
                    message.To.Add(new MailboxAddress(to.FirstName + " " + to.LastName, to.Email));
                }

                foreach (var cc in ccAddress)
                {
                    message.Cc.Add(new MailboxAddress(cc.FirstName + " " + cc.LastName, cc.Email));
                }

                //For Testing only remove in production
                message.Cc.Add(new MailboxAddress("phen@shellsoft.com.ph"));

                message.Subject = subject;

                var builder = new BodyBuilder();

                // Set the plain-text version of the message text
                builder.TextBody = body;
                builder.HtmlBody = body;

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("powerbi@shellsoft.com.ph", "*3-hO3Lr");
                    //client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        public void TestSend()
        {
            try
            {

                //string toAddress = "phen@shellsoft.com.ph";
                //string toAdressTitle = "phen";

                //Smtp Server  
                string SmtpServer = "smtp.office365.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC PO", "stchelpdesk@shellsoft.com.ph"));
                message.To.Add(new MailboxAddress("phen@shellsoft.com.ph"));

                message.Subject = "Test";

                var builder = new BodyBuilder();


                var html = @"";

                // Set the plain-text version of the message text
                builder.HtmlBody = html;
                

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();
                
                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        private string PopulateInlineImages(MimeMessage newMessage, string bodyHtml)
        {
            // encode as base64 for easier downloading/storage 
            if (bodyHtml != null)
            {
                foreach (MimePart att in newMessage.BodyParts)
                {
                    if (att.ContentId != null && att.ContentObject != null && att.ContentType.MediaType == "image" && (bodyHtml.IndexOf("cid:" + att.ContentId) > -1))
                    {
                        byte[] b;
                        using (var mem = new MemoryStream())
                        {
                            att.ContentObject.DecodeTo(mem);
                            b = mem.ToArray();
                        }
                        string imageBase64 = "data:" + att.ContentType.MimeType + ";base64," + System.Convert.ToBase64String(b);
                        bodyHtml = bodyHtml.Replace("cid:" + att.ContentId, imageBase64);
                    }
                }
            }
            return bodyHtml;
        }

        public string GetServer()
        {
            return SERVER;
        }

        public string GetApprover()
        {
            return APPROVER;
        }

        public string GetSharePointSite()
        {
            return SHAREPOINT_SITE;
        }

        public void SendPOApproval(POPending pOPending)
        {
            try
            {

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC Pipeline", "powerbi@shellsoft.com.ph"));
                message.To.Add(new MailboxAddress(pOPending.ApproverEmail));
                message.Subject = "PO #" + pOPending.OrderNumber.ToString() + " requires your approval";

                StringBuilder messageBuilder = new StringBuilder();

                messageBuilder.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                ");

                messageBuilder.Append(POEmailTemplate(pOPending));

                messageBuilder.AppendFormat(EN_US, @"<p style='width: 100%; height: 22px; text-align: left;'>&nbsp;<span style='font-family: arial, helvetica, sans-serif;'>Requested by: {0} </span></p>", pOPending.CreatedByName);

                messageBuilder.AppendFormat(@"
                <p>&nbsp;</p>
                <table style='border-collapse: collapse; width: 100%;' border='0'>
                <tbody>
                <tr>
                <td style='width: 33.3333%; text-align: center;'><span style='font-family: arial, helvetica, sans-serif;'><a href='{0}/po/reject/{1}' target='_blank' rel='noopener' aria-invalid='true'>Reject</a></span></td>
                <td style='width: 33.3333%;'>&nbsp;</td>
                <td style='width: 33.3333%; text-align: center;'><span style='font-family: arial, helvetica, sans-serif;'><a href='{0}/po/approve/{1}' target='_blank' rel='noopener' aria-invalid='true'>Approve</a></span></td>
                </tr>
                </tbody>
                </table>
                <p><span style='color: #7e8c8d; font-size: 10pt; font-family: arial, helvetica, sans-serif;'><em>This is system generated message, please do not reply.</em></span></p>
                </body>
                </html>", SERVER,pOPending.Guid);

                var builder = new BodyBuilder();
                builder.HtmlBody = messageBuilder.ToString();

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("powerbi@shellsoft.com.ph", "*3-hO3Lr");
                    //client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        public void SendPORejectNotification(POGuidStatus pOGuidStatus)
        {
            var po = JsonConvert.DeserializeObject<POPending>(pOGuidStatus.POData);
            po.POPendingItems = JsonConvert.DeserializeObject<POPendingItem[]>(po.POPendingItemsJsonString);

            try
            {

                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC Pipeline", "powerbi@shellsoft.com.ph"));
                message.To.Add(new MailboxAddress(pOGuidStatus.RequestorEmail));
                message.Subject = "PO #" + po.OrderNumber.ToString() + " has been rejected";

                StringBuilder messageBuilder = new StringBuilder();

                messageBuilder.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                ");

                messageBuilder.AppendFormat(@"
                <table style='border-collapse: collapse; width: 100%; height: 88px;' border='0'>
                <tbody>
                    <tr style='height: 22px;'>
                        <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'>{0} rejected this PO.</span></td>
                    </tr>
                    <tr style='height: 22px;'>
                        <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'></span></td>
                    </tr>
                </tbody>
                </table>
                <p>Below are the details of the PO:</p>
                ", po.ApproverName);

                messageBuilder.Append(POEmailTemplate(po));

                messageBuilder.AppendFormat(EN_US, @"<p style='width: 100%; height: 22px; text-align: left;'>&nbsp;<span style='font-family: arial, helvetica, sans-serif;'>Requested by: {0} </span></p>", po.CreatedByName);


                messageBuilder.AppendFormat(@"
                <p>&nbsp;</p>
                <p><span style='color: #7e8c8d; font-size: 10pt; font-family: arial, helvetica, sans-serif;'><em>This is system generated message, please do not reply.</em></span></p>
                </body>
                </html>");

                var builder = new BodyBuilder();
                builder.HtmlBody = messageBuilder.ToString();

                // We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(@"C:\Users\phen.STC-SJSG\Documents\Apps.xlsx");

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("powerbi@shellsoft.com.ph", "*3-hO3Lr");
                    //client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        public void SendPOToDistirubtionList(POGuidStatus pOGuidStatus, ICollection<POGuidStatusAttachment> attachments, POStatus pOStatus)
        {
            var po = JsonConvert.DeserializeObject<POPending>(pOGuidStatus.POData);
            po.POPendingItems = JsonConvert.DeserializeObject<POPendingItem[]>(po.POPendingItemsJsonString);

            try
            {
                //Smtp Server  
                string SmtpServer = "smtp.live.com";
                //Smtp Port Number  
                int SmtpPortNumber = 587;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("STC Pipeline", "powerbi@shellsoft.com.ph"));

                var TOs = JsonConvert.DeserializeObject<string[]>(pOGuidStatus.SendTOs);

                foreach (var t in TOs)
                {
                    message.To.Add(new MailboxAddress(t));
                }

                message.Subject = "";

                switch (pOStatus)
                {
                    case POStatus.Approved:
                        message.Subject = "PO #" + po.OrderNumber.ToString() + " has been approved";
                        break;
                    case POStatus.Cancelled:
                        message.Subject = "PO #" + po.OrderNumber.ToString() + " has been cancelled";
                        break;
                }

                StringBuilder messageBuilder = new StringBuilder();

                messageBuilder.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                ");

                messageBuilder.Append(POEmailTemplate(po));

                messageBuilder.AppendFormat(EN_US, @"<p style='width: 100%; height: 22px; text-align: left;'>&nbsp;<span style='font-family: arial, helvetica, sans-serif;'>Requested by: {0} </span></p>", po.CreatedByName);


                messageBuilder.AppendFormat(@"
                <p>&nbsp;</p>
                <p><span style='color: #7e8c8d; font-size: 10pt; font-family: arial, helvetica, sans-serif;'><em>This is system generated message, please do not reply.</em></span></p>
                </body>
                </html>");

                var builder = new BodyBuilder();
                builder.HtmlBody = messageBuilder.ToString();

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        builder.Attachments.Add(item.Name, item.File);
                    }
                }


                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("powerbi@shellsoft.com.ph", "*3-hO3Lr");
                    //client.Authenticate("stchelpdesk@shellsoft.com.ph", "+a5razAw");
                    client.Send(message);
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

        private string POEmailTemplate(POPending pOPending)
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendFormat(EN_US, @"
                <table style='border-collapse: collapse; width: 100%; height: 88px;' border='0'>
                <tbody>
                <tr style='height: 22px;'>
                <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Supplier</strong></span></td>
                <td style='width: 44.5095%; height: 22px;'>{0}</td>
                <td style='width: 20.4439%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>PO No.</strong></span></td>
                <td style='width: 18.3411%; height: 22px;'>{1}</td>
                </tr>
                ", pOPending.SupplierName, pOPending.OrderNumber);

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                    <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Address</strong></span></td>
                    <td style='width: 44.5095%; height: 22px; vertical-align: top;' rowspan='2'>{0}</td>
                    <td style='width: 20.4439%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Ref No.</strong></span></td>
                    <td style='width: 18.3411%; height: 22px;'>{1}</td>
                </tr>", pOPending.SupplierAddress, pOPending.ReferenceNumber);

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                <td style='width: 16.7055%; height: 22px;'></td>
                <td style='width: 20.4439%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Date</strong></span></td>
                <td style='width: 18.3411%; height: 22px;'>{0}</td>
                </tr>", pOPending.ApprovedOn == null ? "" : ((DateTime)pOPending.ApprovedOn).ToString("MM/dd/yyyy", EN_US));

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Contact Person</strong></span></td>
                <td style='width: 44.5095%; height: 22px;'>{0}</td>
                <td style='width: 20.4439%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>ETA</strong></span></td>
                <td style='width: 18.3411%; height: 22px;'>{1}</td>
                </tr>", pOPending.ContactPersonName, pOPending.EstimatedArrival == null ? "" : ((DateTime)pOPending.EstimatedArrival).ToString("MM/dd/yyyy", EN_US));

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                <td style='width: 16.7055%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Customer</strong></span></td>
                <td style='width: 44.5095%; height: 22px;'>{0}</td>
                <td style='width: 20.4439%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Currency</strong></span></td>
                <td style='width: 18.3411%; height: 22px;'>{1}</td>
                </tr>
                </tbody>
                </table>", pOPending.CustomerName, pOPending.Currency);

            messageBuilder.Append(@"<br/><table style='border-collapse: collapse; width: 100%; height: 110px;' border='0'>
                <tbody>
                <tr style='height: 22px;'>
                <td style='width: 8.55188%; text-align: center; height: 22px;background-color: #ced4d9;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Quantity</strong></span></td>
                <td style='width: 56.3469%; height: 22px;background-color: #ced4d9;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Description</strong></span></td>
                <td style='width: 17.0143%; height: 22px; text-align: right;background-color: #ced4d9;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Unit Price</strong></span></td>
                <td style='width: 18.087%; height: 22px; text-align: right;background-color: #ced4d9;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Amount</strong></span></td>
                </tr>");

            decimal total = 0;
            foreach (var item in pOPending.POPendingItems)
            {
                decimal amount = item.Quantity * item.Price;
                messageBuilder.AppendFormat(EN_US, @"
                    <tr style='height: 22px;'>
                    <td style='width: 8.55188%; text-align: center; height: 22px;border-bottom: 1px solid black;'>{0}</td>
                    <td style='width: 56.3469%; height: 22px;border-bottom: 1px solid black;'>{1}</td>
                    <td style='width: 17.0143%; height: 22px; text-align: right;border-bottom: 1px solid black;'>{2}</td>
                    <td style='width: 18.087%; height: 22px; text-align: right;border-bottom: 1px solid black;'><strong>{3}</strong></td>
                    </tr>
                    ", String.Format(EN_US, "{0:n0}", item.Quantity), item.Name, String.Format(EN_US, "{0:n}", item.Price), String.Format(EN_US, "{0:n}", amount));
                total += amount;
            }

            messageBuilder.AppendFormat(EN_US, @"<tr style='height: 22px;'>
                <td style='width: 8.55188%; text-align: center; height: 22px;'>&nbsp;</td>
                <td style='width: 56.3469%; height: 22px;'>&nbsp;</td>
                <td style='width: 17.0143%; height: 22px; text-align: right;'>&nbsp;</td>
                <td style='width: 18.087%; height: 22px; text-align: right;'><strong>{0}</strong></td>
                </tr>", String.Format(EN_US, "{0:n}", total));

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                <td style='width: 8.55188%; text-align: center; height: 22px;'>&nbsp;</td>
                <td style='width: 56.3469%; height: 22px; text-align: right;'></td>
                <td style='width: 17.0143%; height: 22px; text-align: right;'><span style='font-family: arial, helvetica, sans-serif;'>Discount {0}%</span></td>
                <td style='width: 18.087%; height: 22px; text-align: right;'><strong>-{1}</strong></td>
                </tr>", pOPending.Discount, String.Format(EN_US, "{0:n}", (pOPending.Discount / 100) * total));

            messageBuilder.AppendFormat(EN_US, @"
                <tr style='height: 22px;'>
                <td style='width: 8.55188%; text-align: center; height: 22px;'>&nbsp;</td>
                <td style='width: 56.3469%; height: 22px;'>&nbsp;</td>
                <td style='width: 17.0143%; height: 22px; text-align: right;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Total Amount</strong></span></td>
                <td style='width: 18.087%; height: 22px; text-align: right;border-top: 1px solid black;'><strong>{0}{1}</strong></td>
                </tr>
                </tbody>
                </table>", pOPending.Currency, String.Format(EN_US, "{0:n}", total - ((pOPending.Discount / 100) * total)));

            messageBuilder.AppendFormat(EN_US, @"<table style='border-collapse: collapse; width: 100%; height: 132px;' border='0'>
                <tbody>
                <tr style='height: 22px;'>
                <td style='width: 100%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Remarks</strong></span></td>
                </tr>
                <tr style='height: 22px;'>
                <td style='width: 100%; height: 22px;'>{0}</td>
                </tr>
                <tr style='height: 22px;'>
                <td style='width: 100%; height: 22px;'>&nbsp;</td>
                </tr>
                <tr style='height: 22px;'>
                <td style='width: 100%; height: 22px;'><span style='font-family: arial, helvetica, sans-serif;'><strong>Internal note</strong></span></td>
                </tr>
                <tr style='height: 22px;'>
                <td style='width: 100%; height: 22px;'>{1}</td>
                </tr>
                <tr style='height: 22px;'>
                <td></td>
                </tr>
                </tbody>
                </table>", pOPending.Remarks == null ? "" : Regex.Replace(pOPending.Remarks, @"\r\n?|\n", "<br/>"), pOPending.InternalNote == null ? "" : Regex.Replace(pOPending.InternalNote, @"\r\n?|\n", "<br/>"));

            return messageBuilder.ToString();
        }
    }
}
