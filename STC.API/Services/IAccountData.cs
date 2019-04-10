using STC.API.Entities.AccountEntity;
using STC.API.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IAccountData
    {
        Account AddAccount(AccountNewDto accountNewDto);
        ICollection<Account> GetAccounts();
        Account GetAccountById(int accountId);
        Account GetAccountAllData(int accountId);
        void DeleteAccount(Account account);
        void EditAccount(Account account, AccountEditDto accountEditDto);

        AccountContact AddAccountContact(int accountId, AccountContactNewDto accountContactNewDto);
        AccountContact GetAccountContact(int accountContactId);
        void DeleteAccountContact(AccountContact accountContact);
        void EditAccountContact(AccountContact accountContact, AccountContactEditDto accountContactEditDto);

        AccountIndustry AddIndustry(AccountIndustryNewDto newIndustry);
        ICollection<AccountIndustry> GetAllIndustry();

    }
}