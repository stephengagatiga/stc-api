using STC.API.Entities.POEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IPOGuidStatusData
    {
        POGuidStatus GetPOGuid(Guid guid);

        POGuidStatus UpdatePOStatus(POGuidStatus pOGuidStatus, POStatus pOStatus);

        ICollection<POGuidStatusAttachment> GetPOGuidStatusAttachments(int pOGuidId);
    }
}
