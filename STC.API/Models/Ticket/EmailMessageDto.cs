using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Ticket
{
    public class EmailMessageDto
    {
        public string HeaderFrom { get; set; }
        public string HeaderFromEmail { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; }
    }
}
