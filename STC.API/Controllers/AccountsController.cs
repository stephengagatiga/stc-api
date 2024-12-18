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
using STC.API.Entities.RequestEntity;

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
                AccountSuccessDto accountSucces = _accountData.AddAccount(accountNewDto);
                string[] sendTOs = { accountNewDto.NotifToEmail };
                string[] cc = new string[0];
                FormattableString body = $"<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>  <title></title>  <!--[if !mso]><!-- -->  <meta http-equiv='X-UA-Compatible' content='IE=edge'>  <!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style type='text/css'>  #outlook a {{ padding: 0; }}  .ReadMsgBody {{ width: 100%; }}  .ExternalClass {{ width: 100%; }}  .ExternalClass * {{ line - height:100%; }}  body {{ margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}  table, td {{ border - collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}  img {{ border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}  p {{ display: block; margin: 13px 0; }}</style><!--[if !mso]><!--><style type='text/css'>  @media only screen and (max-width:480px) {{    @-ms-viewport {{ width:320px; }}    @viewport {{ width:320px; }}  }}</style><!--<![endif]--><!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'>  .outlook-group-fix {{    width:100% !important;  }}</style><![endif]--><!--[if !mso]><!-->    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>    <style type='text/css'>        @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);    </style>  <!--<![endif]--><style type='text/css'>  @media only screen and (min-width:480px) {{    .mj - column - per - 100 {{ width:100%!important; }}  }}</style></head><body style='background: #FFFFFF;'>    <div class='mj-container' style='background-color:#FFFFFF;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>        <tr>          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:9px 0px 9px 0px;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>        <tr>          <td style='vertical-align:top;width:600px;'>      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:top;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><p><span style='font-size:22px;'><strong>Account Created</strong></span></p></div></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><table border='0' cellpadding='1' cellspacing='1' style='width:100%;'>	<tbody>		<tr>			<td><span style='font-size:16px;'><strong>Name</strong></span></td>			<td><span style='font-size:16px;'>{accountNewDto.Name}</span></td>		</tr>		<tr>			<td><span style='font-size:16px;'><strong>Code</strong></span></td>			<td><span style='font-size:16px;'>{accountNewDto.Code}</span></td>		</tr>		<tr>			<td><strong><span style='font-size:16px;'>Address</span></strong></td>			<td><span style='font-size:16px;'>{accountNewDto.Address}</span></td>		</tr>		<tr>			<td><strong><span style='font-size:16px;'>Contact Details</span></strong></td>			<td><span style='font-size:16px;'>{accountNewDto.ContactDetails}</span></td>		</tr>		<tr>			<td><strong><span style='font-size:16px;'>Industry</span></strong></td>			<td><span style='font-size:16px;'>{accountSucces.Account.Industry.Name}</span></td>		</tr>		<tr>			<td><strong><span style='font-size:16px;'>Terms of Payments</span></strong></td>			<td><span style='font-size:16px;'>{accountNewDto.TermsOfPayment}</span></td>		</tr>		<tr>			<td><strong><span style='font-size:16px;'>Created By</span></strong></td>			<td><span style='font-size:16px;'>{accountNewDto.NotiifToName}</span></td>		</tr>	</tbody></table></div></td></tr><tr><td style='word-wrap:break-word;font-size:0px;padding:6px 6px 6px 6px;' align='center'><table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:separate;width:auto;' align='center' border='0'><tbody><tr><td style='border:0px solid #000;border-radius:24px;color:#fff;cursor:auto;padding:11px 32px;' align='center' valign='middle' bgcolor='#013243'><a href='{_utils.GetServer()}/token/{accountSucces.Token.Id}' style='text-decoration:none;background:#013243;color:#fff;font-family:Ubuntu, Helvetica, Arial, sans-serif, Helvetica, Arial, sans-serif;font-size:16px;font-weight:normal;line-height:120%;text-transform:none;margin:0px;' target='_blank'>Click here to approve!</a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></div></body></html>";

                _utils.SendPipelineNotif(sendTOs, cc, "New Account Created - For Approval", FormattableString.Invariant(body));

                return Ok(accountSucces.Account);
            }
            return BadRequest();
        }

        [HttpGet]
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

        [HttpGet("industries")]
        public IActionResult GetAllIndustry()
        {
            return Ok(_accountData.GetAllIndustry());
        }
    }
}