using STC.API.Entities.CashReimbursementEntity;
using STC.API.Models.CashReimbursement;
using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Entities.CashReimbursement;

namespace STC.API.Services
{
    public interface ICashReimbursementData
    {
        ExpenseAndCategoryDto GetExpenses();
        Expense AddExpense(AddExpenseDto expenseDto);
        ExpenseCategory AddExpenseCategory(AddExpenseCategoryDto expenseCategoryDto);
        void EditExpense();
        UserReimbursement AddReimbursement(AddUserReimbursement userReimbursment, Reimbursee reimbursee, User createdBy);
        UserReimbursement EditReimbursement(UserReimbursement userReimbursment, EditUserReimbursement editUserReimbursement);
        UserReimbursement GetReimbursement(int id);
        UserReimbursement GetReimbursementWithIncludes(int id);

        ICollection<UserReimbursement> GetTodayReimbursement();
        ICollection<UserReimbursement> GetUnprocessedReimbursement(int createdBy);
        ICollection<UserReimbursement> GetProcessedReimbursement();
        ICollection<UserExpense> GetProcessedUserExpense(int processBy);
        string[] ExtractReimbursements();
        void ProcessedReimbursement(ProcessedDto processed);
        void DeleteReimbursement(UserReimbursement userReimbursement);
    }
}
