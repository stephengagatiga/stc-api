using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.AccountEntity;
using STC.API.Models.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace STC.API.Services
{
    public class SqlAccountData : IAccountData
    {
        private STCDbContext _context;

        public SqlAccountData(STCDbContext context)
        {
            _context = context;
        }

        public Account AddAccount(AccountNewDto accountNewDto)
        {
            var account = new Account {
                Name = accountNewDto.Name,
                Code = accountNewDto.Code,
                Address = accountNewDto.Address,
                ContactDetails = accountNewDto.ContactDetails,
                AccountIndustryId = accountNewDto.AccountIndustryId,
                TermsOfPayment = accountNewDto.TermsOfPayment,
                CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"))
            };
            _context.Accounts.Add(account);
            _context.Entry(account).State = EntityState.Added;
            SaveChanges();
            return account;
        }
        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            _context.Entry(account).State = EntityState.Deleted;
            SaveChanges();
        }
        public void EditAccount(Account account, AccountEditDto accountEditDto)
        {
            account.Name = accountEditDto.Name;
            account.Code = accountEditDto.Code;
            account.Address = accountEditDto.Address;
            account.ContactDetails = accountEditDto.ContactDetails;
            account.AccountIndustryId = accountEditDto.AccountIndustryId;
            account.TermsOfPayment = accountEditDto.TermsOfPayment;
            _context.Accounts.Update(account);
            _context.Entry(account).State = EntityState.Modified;
            SaveChanges();
        }
        public Account GetAccountAllData(int accountId)
        {
            var account = _context.Accounts.Where(a => a.Id == accountId)
                                .Include(ac => ac.AccountContacts)
                                .FirstOrDefault();
            return account;
        }
        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == accountId);
        }
        public ICollection<Account> GetAccounts()
        {
            var accounts = _context.Accounts.OrderBy(a => a.Name)
                            .Include(ac => ac.Industry)    
                            .ToList();
            return accounts;
        }



        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public AccountContact AddAccountContact(int accountId, AccountContactNewDto accountContactNewDto)
        {

            var accountContact = new AccountContact
            {
                FirstName = accountContactNewDto.FirstName,
                LastName = accountContactNewDto.LastName,
                ContactDetails = accountContactNewDto.ContactDetails,
                Email = accountContactNewDto.Email,
                Designation = accountContactNewDto.Designation,
                PrimaryContact = accountContactNewDto.PrimaryContact,
                AccountId = accountId,
                CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")),
                Active = true
            };

            _context.AccountContacts.Add(accountContact);
            _context.Entry(accountContact).State = EntityState.Added;
            _context.SaveChanges();
            return accountContact;
        }

        public AccountContact GetAccountContact(int accountContactId)
        {
            var accountContact = _context.AccountContacts.FirstOrDefault(ac => ac.Id == accountContactId);
            return accountContact;
        }
        public void DeleteAccountContact(AccountContact accountContact)
        {
            _context.AccountContacts.Remove(accountContact);
            _context.Entry(accountContact).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void EditAccountContact(AccountContact accountContact, AccountContactEditDto accountContactEditDto)
        {
            accountContact.FirstName = accountContactEditDto.FirstName;
            accountContact.LastName = accountContactEditDto.LastName;
            accountContact.Email = accountContactEditDto.Email;
            accountContact.ContactDetails = accountContactEditDto.ContactDetails;
            accountContact.Designation = accountContactEditDto.Designation;
            accountContact.PrimaryContact = accountContactEditDto.PrimaryContact;
            accountContact.Active = accountContactEditDto.Active;
            _context.AccountContacts.Update(accountContact);
            _context.Entry(accountContact).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public AccountIndustry AddIndustry(AccountIndustryNewDto newIndustry)
        {
            try
            {
                var industry = new AccountIndustry { Name = newIndustry.Name };
                _context.AccountIndustries.Add(industry);
                _context.Entry(industry).State = EntityState.Added;
                _context.SaveChanges();
                return industry;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public ICollection<AccountIndustry> GetAllIndustry()
        {
            return _context.AccountIndustries.OrderBy(a => a.Name).ToList();
        }
    }
}
