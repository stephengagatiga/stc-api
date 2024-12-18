using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.CashReimbursementEntity;
using STC.API.Models.CashReimbursement;

namespace STC.API.Services
{
    public class SqlReimburseeData : IReimburseeData
    {
        private STCDbContext _context;

        public SqlReimburseeData(STCDbContext context)
        {
            _context = context;
        }

        public Reimbursee AddReimbursee(AddReimburseeDto reimbursee)
        {
            Reimbursee newReimbursee = new Reimbursee()
            {
                FirstName = reimbursee.FirstName,
                LastName = reimbursee.LastName,
                BankAccountNumber = reimbursee.BankAccountNumber,
                CreatedOn = DateTime.Now
            };

            _context.Reimbursees.Add(newReimbursee);
            _context.Entry(newReimbursee).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return newReimbursee;
        }

        public Reimbursee CheckIfReimburseeExist(AddReimburseeDto reimbursee)
        {
            return _context.Reimbursees.FirstOrDefault(r => (r.FirstName.ToUpper() == reimbursee.FirstName.ToUpper() && r.LastName.ToUpper() == reimbursee.LastName.ToUpper()) || r.BankAccountNumber == reimbursee.BankAccountNumber);
        }

        public Reimbursee EditReimbursee(EditReimburseeDto reimbursee)
        {
            throw new NotImplementedException();
        }

        public Reimbursee GetReimbursee(int reimburseeId)
        {
            return _context.Reimbursees.FirstOrDefault(r => r.Id == reimburseeId);
        }

        public ICollection<Reimbursee> GetReimbursees()
        {
            return _context.Reimbursees.OrderBy(r => r.FirstName).ToList();
        }
    }
}
