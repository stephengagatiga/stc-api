using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.ComponentEntity
{
    public enum Status
    {
        Open,
        Approved,
        ForApproval,
        ForRevision,
        Rejected,
        Closed,
        Archived,
        Lost
    }
}
