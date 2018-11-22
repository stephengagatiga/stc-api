using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.TicketEntity
{
    public enum TicketStatus
    {
        Open = 0,
        Pending = 1,
        Suspended = 2,
        Solved = 3,
        Closed = 4
    }
}
