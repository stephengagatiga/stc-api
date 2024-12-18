using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.POEntity
{
    public enum POStatus
    {
        Pending,
        Approved,
        Rejected,
        Draft,
        Received,
        ForCancellation,
        Cancelled,
        CancellationRejected
    }
}
