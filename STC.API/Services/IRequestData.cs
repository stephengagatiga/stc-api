using STC.API.Entities.RequestEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IRequestData
    {
        Request GetRequest(Guid tokenId);
        Object Approve(Request token);
        Object Reject(Request token);
    }
}
