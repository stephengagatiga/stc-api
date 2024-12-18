using STC.API.Entities.POEntity;
using STC.API.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IPOPendingData
    {
        POPending AddPO(NewPOPending pOPending);
        POPending AddPODraft(NewPOPendingDraft pOPendingDraft);
        POPending GetPO(int poId);
        POPending GetPOByGuid(Guid guid);
        void UpdatePOStatus(POPending pOPending, POStatus pOStatus);
        POPending UpdatePO(EditPOPending newPOPending, POPending oldPOPending);
        POPending UpdatePO(EditPOPendingDraft newPOPending, POPending oldPOPending);
        ICollection<POPending> GetPOs();
    }
}
