using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.CashReimbursement;
using STC.API.Entities.CashReimbursementEntity;
using STC.API.Entities.UserEntity;
using STC.API.Models.CashReimbursement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STC.API.Services
{
    public class SqlCashReimbursementData : ICashReimbursementData
    {
        private STCDbContext _context;

        public SqlCashReimbursementData(STCDbContext context)
        {
            _context = context;
        }

        public Expense AddExpense(AddExpenseDto expenseDto)
        {
            var expense = new Expense()
            {
                Name = expenseDto.Name,
                ExpenseCategoryId = expenseDto.CategoryId
            };

            _context.Expense.Add(expense);
            _context.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return expense;
        }

        public ExpenseCategory AddExpenseCategory(AddExpenseCategoryDto expenseCategoryDto)
        {
            var expenseCategory = new ExpenseCategory()
            {
                Name = expenseCategoryDto.Name
            };

            _context.ExpenseCategory.Add(expenseCategory);
            _context.Entry(expenseCategory).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return expenseCategory;
        }

        public UserReimbursement AddReimbursement(AddUserReimbursement userReimbursment, Reimbursee reimbursee, User createdBy)
        {
            var now = DateTime.Now;
            var reimbursement = new UserReimbursement()
            {
                Reimbursee = reimbursee,
                CreatedOn = now,
                CreatedBy = createdBy,
                ReimbursementDate = userReimbursment.ReimbursementDate,
                ReimbursementStatus = ReimbursementStatus.APPROVED
            };

            reimbursement.UserExpenses = new List<UserExpense>();

            foreach (var e in userReimbursment.Expenses)
            {
                var expense = new UserExpense();
                expense.ExepenseId = e.ExpenseId;
                expense.Amount = e.Amount;
                expense.Date = e.Date;
                expense.Remarks = e.Remarks;
                reimbursement.UserExpenses.Add(expense);
            }

            _context.UserReimbursements.Add(reimbursement);
            _context.Entry(reimbursement).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return reimbursement;
        }

        public void DeleteReimbursement(UserReimbursement userReimbursement)
        {
            foreach (var item in _context.UserExpenses.Where(r => r.UserReimbursement == userReimbursement).ToList())
            {
                _context.UserExpenses.Remove(item);
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.UserReimbursements.Remove(userReimbursement);
            _context.Entry(userReimbursement).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void EditExpense()
        {
            throw new NotImplementedException();
        }

        public UserReimbursement EditReimbursement(UserReimbursement userReimbursment, EditUserReimbursement editUserReimbursement)
        {
            foreach(var ue in _context.UserExpenses.Where(u => u.UserReimbursementId == userReimbursment.Id).ToList())
            {
                _context.UserExpenses.Remove(ue);
                _context.Entry(ue).State = EntityState.Deleted;
            }


            userReimbursment.ReimbursementDate = editUserReimbursement.ReimbursementDate;

            _context.UserReimbursements.Update(userReimbursment);
            _context.Entry(userReimbursment).State = EntityState.Modified;


            foreach (var e in editUserReimbursement.Expenses)
            {
                var expense = new UserExpense();
                expense.ExepenseId = e.ExpenseId;
                expense.Amount = e.Amount;
                expense.UserReimbursementId = userReimbursment.Id;
                expense.Remarks = e.Remarks;
                expense.Date = e.Date;
                _context.UserExpenses.Add(expense);
                _context.Entry(expense).State = EntityState.Added;
            }


            _context.SaveChanges();

            return userReimbursment;
        }

        public string[] ExtractReimbursements()
        {
            List<string> extract = new List<string>();

      
            var userReimbursement = _context.UserReimbursements.Where(r => r.ReimbursementStatus == ReimbursementStatus.APPROVED)
                  .Include(r => r.Reimbursee)
                  .Include(r => r.CreatedBy)
                  .Include(r => r.UserExpenses).ThenInclude(e => e.Expense).ThenInclude(c => c.ExpenseCategory)
                  .OrderBy(r => r.Reimbursee.FirstName)
                  .ToList();

            List<Reimbursee> reimbursees = new List<Reimbursee>();
            foreach (var r in userReimbursement)
            {
                if (!reimbursees.Contains(r.Reimbursee))
                {
                    reimbursees.Add(r.Reimbursee);
                }
            }

            foreach (var u in reimbursees)
            {
                var bankAccountNo = u.BankAccountNumber;
                decimal amount = 0;
                foreach (var e in userReimbursement.Where(ur => ur.ReimburseeId == u.Id))
                {
                    amount += e.UserExpenses.Sum(s => s.Amount); ;
                }
                extract.Add(bankAccountNo + "\t" + amount);
            }

            return extract.ToArray();
        }

        public ExpenseAndCategoryDto GetExpenses()
        {
            var expenses = _context.Expense.OrderBy(e => e.Name).ToList();
            var categories = _context.ExpenseCategory.OrderBy(c => c.Name).ToList();

            return new ExpenseAndCategoryDto()
            {
                Expense = expenses,
                ExpenseCategory = categories
            };
        }

        public ICollection<UserReimbursement> GetProcessedReimbursement()
        {
            //var thisYear = new DateTime(DateTime.Now.Year - 1, 11, 1);
            var thisYear = new DateTime(DateTime.Now.Year, 1, 1);
            // minus 1 month if current first month
            if (DateTime.Today.Month == 1)
            {
                thisYear = thisYear.Subtract(new TimeSpan(31, 0, 0, 0, 0));
            }

            var result = _context.UserReimbursements.Where(r => r.ReimbursementStatus == ReimbursementStatus.PROCESSED && r.ProcessOn > thisYear && r.CreatedOn > thisYear)
                      .Include(r => r.Reimbursee)
                      .Include(r => r.CreatedBy)
                      .Include(r => r.ProcessBy)
                      .Include(r => r.UserExpenses).ThenInclude(e => e.Expense).ThenInclude(c => c.ExpenseCategory)
                      .OrderByDescending(r => r.CreatedOn)
                      .ToList();
            return result;
        }

        public ICollection<UserExpense> GetProcessedUserExpense(int processBy)
        {
            var thisYear = new DateTime(DateTime.Now.Year, 1, 1);
            // minus 1 month if current first month
            if (DateTime.Today.Month == 1)
            {
                thisYear = thisYear.Subtract(new TimeSpan(31, 0, 0, 0, 0));
            }

            return _context.UserExpenses
                                .Include(u => u.Expense)
                                .Include(u => u.UserReimbursement).ThenInclude(u => u.Reimbursee)
                                .Where(r =>  r.UserReimbursement.ProcessById == processBy && r.UserReimbursement.CreatedOn > thisYear)
                                .OrderByDescending(r => r.UserReimbursement.CreatedOn)
                                .ToList();


        }

        public UserReimbursement GetReimbursement(int id)
        {
            return _context.UserReimbursements
                            .FirstOrDefault(u => u.Id == id);
        }

        public UserReimbursement GetReimbursementWithIncludes(int id)
        {

            var tmp = _context.UserReimbursements
                          .Include(r => r.Reimbursee)
                          .Include(r => r.CreatedBy)
                          .FirstOrDefault(r => r.Id == id);

            tmp.UserExpenses = _context.UserExpenses.Where(u => u.UserReimbursementId == id)
                                        .Include(e => e.Expense).ThenInclude(c => c.ExpenseCategory).ToList();
            return tmp;
        }

        public ICollection<UserReimbursement> GetTodayReimbursement()
        {
            var result = _context.UserReimbursements.Where(r => r.CreatedOn >= DateTime.Today && r.ReimbursementStatus == ReimbursementStatus.APPROVED)
                    .Include(r => r.Reimbursee)
                    .Include(r => r.CreatedBy)
                    .Include(r => r.UserExpenses).ThenInclude(e => e.Expense).ThenInclude(c => c.ExpenseCategory)
                    .OrderByDescending(r => r.CreatedOn)
                    .ToList();
            return result;
        }

        public ICollection<UserReimbursement> GetUnprocessedReimbursement(int createdBy)
        {
            var result = _context.UserReimbursements.Where(r => r.ReimbursementStatus == ReimbursementStatus.APPROVED && r.CreatedById == createdBy)
                   .Include(r => r.Reimbursee)
                   .Include(r => r.CreatedBy)
                   .Include(r => r.UserExpenses).ThenInclude(e => e.Expense).ThenInclude(c => c.ExpenseCategory)
                   .ToList();
            return result;
        }

        public void ProcessedReimbursement(ProcessedDto processed)
        {
            var now = DateTime.Now;
            var p = _context.UserReimbursements.Where(r => r.ReimbursementStatus == ReimbursementStatus.APPROVED);
            foreach (var item in p)
            {
                item.ReferenceNumber = processed.ReferenceNumber;
                item.ProcessById = processed.Id;
                item.ProcessOn = now;
                item.ReimbursementStatus = ReimbursementStatus.PROCESSED;

                _context.UserReimbursements.Update(item);
                _context.Entry(item).State = EntityState.Modified;
            }

            _context.SaveChanges();

        }
    }
}
