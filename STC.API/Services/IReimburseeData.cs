using STC.API.Entities.CashReimbursementEntity;
using STC.API.Models.CashReimbursement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IReimburseeData
    {
        Reimbursee CheckIfReimburseeExist(AddReimburseeDto reimbursee);
        Reimbursee AddReimbursee(AddReimburseeDto reimbursee);
        Reimbursee EditReimbursee(EditReimburseeDto reimbursee);
        Reimbursee GetReimbursee(int reimburseeId);
        ICollection<Reimbursee> GetReimbursees();

    }
}
