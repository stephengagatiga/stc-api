using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Ticket
{
    public class SendReplyDto
    {
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Attachments { get; set; }
        public string Message { get; set; }
    }
}
