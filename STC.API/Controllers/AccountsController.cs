using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Account;
using STC.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using STC.API.Entities.AccountEntity;
using Microsoft.AspNetCore.Authorization;

namespace STC.API.Controllers
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private IAccountData _accountData;
        private IProductData _productData;
        private UserManager<IdentityUser> _userManager;
        private IUtils _utils;

        public AccountsController(IAccountData accountData, IProductData productData, UserManager<IdentityUser> userManager, IUtils utils)
        {
            _accountData = accountData;
            _productData = productData;
            _userManager = userManager;
            _utils = utils;
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountNewDto accountNewDto)
        {
            if (ModelState.IsValid)
            {
                var account = _accountData.AddAccount(accountNewDto);
                return Ok(account);
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetAccounts()
        {
            return Ok(_accountData.GetAccounts());
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
            var account = _accountData.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound();
            }

            _accountData.DeleteAccount(account);
            return NoContent();
        }

        [HttpPost("{accountId}")]
        public IActionResult EditAccount(int accountId, [FromBody] AccountEditDto accountEditDto)
        {
            if (ModelState.IsValid)
            {
                var account = _accountData.GetAccountById(accountId);
                if (account == null)
                {
                    return NotFound();
                }

                _accountData.EditAccount(account, accountEditDto);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccount(int accountId)
        {
            var account = _accountData.GetAccountAllData(accountId);
            if (account == null)
            {
                return StatusCode(404, "Account not found!");
            }
            return Ok(account);
        }

        [HttpGet("{accountId}/contacts")]
        public IActionResult GetAccountContacts(int accountId)
        {
            var account = _accountData.GetAccountAllData(accountId);
            if (account == null)
            {
                return StatusCode(404, "Account not found!");
            }
            return Ok(account.AccountContacts);
        }

        [HttpPost("{accountId}/accountcontacts")]
        public async Task<IActionResult> AddAccountContact(int accountId, [FromBody] AccountContactNewDto accountContactNewDto)
        {
            if (ModelState.IsValid)
            {
                var account = _accountData.GetAccountById(accountId);

                if (account == null)
                {
                    return StatusCode(404, "Account not found!");
                }

                var user = new IdentityUser()
                {
                    UserName = accountContactNewDto.Email,
                    Email = accountContactNewDto.Email
                };

                var findEmail = await _userManager.FindByEmailAsync(accountContactNewDto.Email);

                if (findEmail != null)
                {
                    return StatusCode(400, "Email already exist");
                }
                var password = _utils.GeneratePassword();
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {

                    var accountContact = _accountData.AddAccountContact(accountId, accountContactNewDto);

                    if (accountContact != null)
                    {
                        await _utils.SendClientAccountCredentialAsync(accountContactNewDto.FirstName, accountContactNewDto.Email, password);
                        return Ok(accountContact);
                    }
                }

                return StatusCode(500, "Internal Server Error");
                
            }
            return BadRequest();
        }

        [HttpDelete("{accountId}/accountcontacts/{accountContactId}")]
        public IActionResult DeleteAccountContact(int accountId, int accountContactId)
        {
            var account = _accountData.GetAccountById(accountId);
            if (account == null)
            {
                return StatusCode(404, "Account not found!");
            }

            var accountContact = _accountData.GetAccountContact(accountContactId);
            if (accountContact == null)
            {
                return StatusCode(404, "Account contact not found!");
            }

            _accountData.DeleteAccountContact(accountContact);
            return NoContent();
        }

        [HttpPost("{accountId}/accountcontacts/{accountContactId}")]
        public IActionResult EditAccountContact(int accountId, int accountContactId, [FromBody] AccountContactEditDto accountContactEditDto)
        {
            if (ModelState.IsValid)
            {
                var account = _accountData.GetAccountById(accountId);
                if (account == null)
                {
                    return StatusCode(404, "Account not found!");
                }

                var accountContact = _accountData.GetAccountContact(accountContactId);
                if (accountContact == null)
                {
                    return StatusCode(404, "Account contact not found!");
                }

                _accountData.EditAccountContact(accountContact, accountContactEditDto);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("industry")]
        public IActionResult AddIndustry([FromBody] AccountIndustryNewDto accountIndustryNewDto)
        {
            if (ModelState.IsValid)
            {
                var industry = _accountData.AddIndustry(accountIndustryNewDto);
                if (industry == null)
                {
                    return StatusCode(500);
                }
                return Ok(industry);
            }
            return BadRequest();
        }
    }
}