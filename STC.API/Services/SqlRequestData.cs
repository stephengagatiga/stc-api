using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using STC.API.Data;
using STC.API.Entities.ComponentEntity;
using STC.API.Entities.RequestEntity;
using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class SqlRequestData : IRequestData
    {
        private STCDbContext _context;
        private IUtils _utils;

        public SqlRequestData(STCDbContext context, IUtils utils)
        {
            _context = context;
            _utils = utils;
        }

        public Object Approve(Request token)
        {
            Status status = Status.Approved;
            
            if (token.RequestSubject == RequestSubject.OPPORTUNITY || token.RequestSubject == RequestSubject.COMPONENT)
            {
                status = Status.Open;
            }

            return ProcessRequest(token, status, RequestAction.APPROVED);
        }

        public Request GetRequest(Guid tokenId)
        {
            var token = _context.Request.FirstOrDefault(t => t.Id == tokenId);
            return token;
        }

        public Object Reject(Request token)
        {
            return ProcessRequest(token, Status.Rejected, RequestAction.DENIED);
        }

        private Object ProcessRequest(Request request, Status status, RequestAction requestAction)
        {
            DateTime now = DateTime.Now;

            if (request.RequestSubject == RequestSubject.OPPORTUNITY)
            {
                var opportunity = _context.Opportunities.Where(o => o.RequestId == request.Id)
                                            .Include(o => o.Account)
                                            .Include(o => o.Components)
                                            .ThenInclude(c => c.Stage)
                                            .FirstOrDefault();


                if (opportunity == null)
                {
                    return null;
                }

                List<User> TOs = new List<User>();
                var sa = _context.Users.FirstOrDefault(u => u.Id == opportunity.CreatedById);
                TOs.Add(sa);


                //The following code is used to update the status of the opportunity base on the overall status of its components
                int countOpenComponent = 0;
                int countForApprovalComponent = 0;
                int countRejectedComponent = 0;
                int countLostComponent = 0;
                int countClosedComponent = 0;

                foreach (var component in opportunity.Components)
                {
                    var componentToken = _context.Request.FirstOrDefault(t => t.Id == component.RequestId);

                    //Apply only to those component with status of For Approval
                    if (componentToken != null && component.Status == Status.ForApproval)
                    {

                        if (TOs.FirstOrDefault(u => u.Id == component.CreatedById) == null)
                        {
                            var tmpUser = _context.Users.FirstOrDefault(u => u.Id == opportunity.CreatedById);
                            TOs.Add(tmpUser);
                        }

                        if (component.Stage.Percentage == 0 && status != Status.Rejected)
                        {
                            component.Status = Status.Lost;
                        }
                        else if (component.Stage.Percentage == 100 && status != Status.Rejected)
                        {
                            component.Status = Status.Closed;
                        }
                        else
                        {
                            component.Status = status;
                        }

                        if (request.RequestType == RequestType.ADD)
                        {
                            componentToken.ImplementedBy = opportunity.CreatedById;
                        } else if (request.RequestType == RequestType.EDIT)
                        {
                            componentToken.ImplementedBy = component.ModifiedById;

                            if (component.ComponentVersionId != null && requestAction == RequestAction.DENIED) 
                            {

                                var componentVersion = new ComponentVersion()
                                {
                                    ComponentId = component.Id,
                                    Added = now,
                                    OpportunityId = component.OpportunityId,
                                    Description = component.Description,
                                    CategoryId = component.CategoryId,
                                    AccountExecutiveId = component.AccountExecutiveId,
                                    SolutionsArchitectId = component.SolutionsArchitectId,
                                    StageId = component.StageId,
                                    ProductId = component.ProductId,
                                    Qty = component.Qty,
                                    PricePerUnit = component.PricePerUnit,
                                    CostPerUnit = component.CostPerUnit,
                                    POC = component.Poc,
                                    Status = component.Status,
                                    Remarks = component.Remarks,
                                    Modified = (DateTime)component.Modified,
                                    ModifiedById = component.ModifiedById,
                                    RequestId = component.RequestId,
                                    VersionNumber = component.VersionNumber,
                                };

                                _context.ComponentVersions.Add(componentVersion);
                                _context.Entry(componentVersion).State = EntityState.Added;


                                var prevComponent = _context.ComponentVersions.FirstOrDefault(c => c.Id == component.ComponentVersionId);

                                component.AccountExecutiveId = prevComponent.AccountExecutiveId;
                                component.CategoryId = prevComponent.CategoryId;
                                component.ComponentTypeId = prevComponent.ComponentTypeId;
                                component.CostPerUnit = prevComponent.CostPerUnit;
                                component.Description = prevComponent.Description;
                                component.Modified = prevComponent.Modified;
                                component.ModifiedById = prevComponent.ModifiedById;
                                component.OpportunityId = prevComponent.OpportunityId;
                                component.Poc = prevComponent.POC;
                                component.PricePerUnit = prevComponent.PricePerUnit;
                                component.ProductId = prevComponent.ProductId;
                                component.Qty = prevComponent.Qty;
                                component.Remarks = prevComponent.Remarks;
                                component.RequestId = prevComponent.RequestId;
                                component.SolutionsArchitectId = prevComponent.SolutionsArchitectId;
                                component.StageId = prevComponent.StageId;
                                component.TargetCloseDate = prevComponent.TargetCloseMonth;
                                component.ValidityDate = prevComponent.Validity;
                                component.VersionNumber = component.VersionNumber + 1;
                                component.Status = prevComponent.Status;

                            }
                        }

                        componentToken.ImplementedOn = now;
                        componentToken.RequestAction = requestAction;

                        _context.Request.Update(componentToken);
                        _context.Entry(request).State = EntityState.Modified;

                        _context.Components.Update(component);
                        _context.Entry(component).State = EntityState.Modified;
                    }

                    if (component.Status == Status.Open)
                    {
                        countOpenComponent++;
                    }
                    else if (component.Status == Status.ForApproval)
                    {
                        countForApprovalComponent++;
                    }
                    else if (component.Status == Status.Rejected)
                    {
                        countRejectedComponent++;
                    }
                    else if (component.Status == Status.Closed)
                    {
                        countClosedComponent++;
                    }
                    else if (component.Status == Status.Lost)
                    {
                        countLostComponent++;
                    }

                }

                //Only change the status of opportunity to Rejected/Open when its all components it rejected
                //Otherwise when some are open and some rejected set the status of opportunity in Open
                if (countForApprovalComponent != 0)
                {
                    opportunity.Status = Status.ForApproval;
                }
                else if (countClosedComponent == opportunity.Components.Count)
                {
                    opportunity.Status = Status.Closed;
                }
                else if (countLostComponent == opportunity.Components.Count)
                {
                    opportunity.Status = Status.Lost;
                }
                else
                {
                    if (countRejectedComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Rejected;
                    }
                    else
                    {
                        opportunity.Status = Status.Open;
                    }
                }

                _context.Opportunities.Update(opportunity);
                _context.Entry(opportunity).State = EntityState.Modified;

                if (request.RequestType == RequestType.ADD)
                {
                    FormattableString body = $"<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>  <title></title>  <!--[if !mso]><!-- -->  <meta http-equiv='X-UA-Compatible' content='IE=edge'>  <!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style type='text/css'>  #outlook a {{ padding: 0; }}  .ReadMsgBody {{ width: 100%; }}  .ExternalClass {{ width: 100%; }}  .ExternalClass * {{ line-height:100%; }}  body {{ margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}  table, td {{ border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}  img {{ border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}  p {{ display: block; margin: 13px 0; }}</style><!--[if !mso]><!--><style type='text/css'>  @media only screen and (max-width:480px) {{    @-ms-viewport {{ width:320px; }}    @viewport {{ width:320px; }}  }}</style><!--<![endif]--><!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'>  .outlook-group-fix {{    width:100% !important;  }}</style><![endif]--><!--[if !mso]><!-->    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>    <style type='text/css'>        @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);    </style>  <!--<![endif]--><style type='text/css'>  @media only screen and (min-width:480px) {{    .mj-column-per-100 {{ width:100%!important; }}  }}</style></head><body style='background: #FFFFFF;'>    <div class='mj-container' style='background-color:#FFFFFF;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>        <tr>          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:9px 0px 9px 0px;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>        <tr>          <td style='vertical-align:top;width:600px;'>      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:top;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><p><span style='font-size:18px;'>Hi,<br><br>Opportunity in <strong>{opportunity.Account.Name} </strong>has been {status.ToString().ToLower()}.</span></p></div></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></div></body></html>";

                    List<User> CCs = new List<User>();

                    _utils.SendPipelineNotif(TOs, CCs, "Opportunity " + status.ToString().ToLower(), FormattableString.Invariant(body));

                    request.ImplementedBy = opportunity.CreatedById;
                    request.ImplementedOn = now;
                    request.RequestAction = requestAction;
                }

                _context.Request.Update(request);
                _context.Entry(request).State = EntityState.Modified;

                _context.SaveChanges();

                return opportunity;
            }
            else if (request.RequestSubject == RequestSubject.COMPONENT)
            {
                var component = _context.Components.Where(c => c.RequestId == request.Id).FirstOrDefault();

                if (component == null)
                {
                    return null;
                }

                var opportunity = _context.Opportunities.Where(o => o.Id == component.OpportunityId)
                        .Include(o => o.Account)
                        .Include(o => o.Components)
                        .ThenInclude(c => c.Stage)
                        .FirstOrDefault();

                if (opportunity == null)
                {
                    return null;
                }

                if (request.RequestType == RequestType.ADD)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == component.CreatedById);

                    request.ImplementedBy = component.CreatedById;
                    request.ImplementedOn = now;
                    request.RequestAction = requestAction;

                    //The following code is used to update the status of the opportunity base on the overall status of its components
                    int countOpenComponent = 0;
                    int countForApprovalComponent = 0;
                    int countRejectedComponent = 0;
                    int countLostComponent = 0;
                    int countClosedComponent = 0;

                    var statusForEmail = "";

                    foreach (var item in opportunity.Components)
                    {
                        if (item.RequestId == request.Id)
                        {
                            if (item.Stage.Percentage == 0 && status != Status.Rejected)
                            {
                                item.Status = Status.Lost;
                                statusForEmail = Status.Lost.ToString().ToLower();
                            }
                            else if (item.Stage.Percentage == 100 && status != Status.Rejected)
                            {
                                item.Status = Status.Closed;
                                statusForEmail = Status.Closed.ToString().ToLower();

                            }
                            else
                            {
                                item.Status = status;
                                statusForEmail = status.ToString().ToLower();
                            }
                        }
 
                        if (item.Status == Status.Open)
                        {
                            countOpenComponent++;
                        }
                        else if (item.Status == Status.ForApproval)
                        {
                            countForApprovalComponent++;
                        }
                        else if (item.Status == Status.Rejected)
                        {
                            countRejectedComponent++;
                        }
                        else if (item.Status == Status.Closed)
                        {
                            countClosedComponent++;
                        }
                        else if (item.Status == Status.Lost)
                        {
                            countLostComponent++;
                        }
                    }

                    if (countForApprovalComponent != 0)
                    {
                        opportunity.Status = Status.ForApproval;
                    }
                    else if (countClosedComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Closed;
                    }
                    else if (countLostComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Lost;
                    }
                    else
                    {
                        if (countRejectedComponent == opportunity.Components.Count)
                        {
                            opportunity.Status = Status.Rejected;
                        } else
                        {
                            opportunity.Status = Status.Open;
                        }
                    }

                    _context.Opportunities.Update(opportunity);
                    _context.Entry(opportunity).State = EntityState.Modified;

                    _context.Request.Update(request);
                    _context.Entry(request).State = EntityState.Modified;

                    _context.SaveChanges();

                    FormattableString body = $"<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>  <title></title>  <!--[if !mso]><!-- -->  <meta http-equiv='X-UA-Compatible' content='IE=edge'>  <!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style type='text/css'>  #outlook a {{ padding: 0; }}  .ReadMsgBody {{ width: 100%; }}  .ExternalClass {{ width: 100%; }}  .ExternalClass * {{ line-height:100%; }}  body {{ margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}  table, td {{ border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}  img {{ border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}  p {{ display: block; margin: 13px 0; }}</style><!--[if !mso]><!--><style type='text/css'>  @media only screen and (max-width:480px) {{    @-ms-viewport {{ width:320px; }}    @viewport {{ width:320px; }}  }}</style><!--<![endif]--><!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'>  .outlook-group-fix {{    width:100% !important;  }}</style><![endif]--><!--[if !mso]><!-->    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>    <style type='text/css'>        @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);    </style>  <!--<![endif]--><style type='text/css'>  @media only screen and (min-width:480px) {{    .mj-column-per-100 {{ width:100%!important; }}  }}</style></head><body style='background: #FFFFFF;'>    <div class='mj-container' style='background-color:#FFFFFF;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>        <tr>          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:9px 0px 9px 0px;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>        <tr>          <td style='vertical-align:top;width:600px;'>      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:top;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><p><span style='font-size:18px;'>Hi,<br><br>New component in <strong>{opportunity.Account.Name} </strong>has been {statusForEmail}.</span></p></div></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></div></body></html>";

                    List<User> CCs = new List<User>();
                    List<User> TOs = new List<User>();
                    TOs.Add(user);

                    _utils.SendPipelineNotif(TOs, CCs, "New component " + statusForEmail, FormattableString.Invariant(body));

                    return opportunity;
                }
                else if (request.RequestType == RequestType.EDIT && requestAction == RequestAction.APPROVED)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == component.CreatedById);

                    request.ImplementedBy = component.CreatedById;
                    request.ImplementedOn = now;
                    request.RequestAction = requestAction;

                    //The following code is used to update the status of the opportunity base on the overall status of its components
                    int countOpenComponent = 0;
                    int countForApprovalComponent = 0;
                    int countRejectedComponent = 0;
                    int countLostComponent = 0;
                    int countClosedComponent = 0;

                    var statusForEmail = "";

                    foreach (var item in opportunity.Components)
                    {
                        if (item.RequestId == request.Id)
                        {
                            if (item.Stage.Percentage == 0)
                            {
                                item.Status = Status.Lost;
                                statusForEmail = Status.Lost.ToString().ToLower();
                            }
                            else if (item.Stage.Percentage == 100)
                            {
                                item.Status = Status.Closed;
                                statusForEmail = Status.Closed.ToString().ToLower();

                            }
                            else
                            {
                                item.Status = status;
                                statusForEmail = status.ToString().ToLower();
                            }
                        }

                        if (item.Status == Status.Open)
                        {
                            countOpenComponent++;
                        }
                        else if (item.Status == Status.ForApproval)
                        {
                            countForApprovalComponent++;
                        }
                        else if (item.Status == Status.Rejected)
                        {
                            countRejectedComponent++;
                        }
                        else if (item.Status == Status.Closed)
                        {
                            countClosedComponent++;
                        }
                        else if (item.Status == Status.Lost)
                        {
                            countLostComponent++;
                        }
                    }

                    if (countForApprovalComponent != 0)
                    {
                        opportunity.Status = Status.ForApproval;
                    }
                    else if (countClosedComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Closed;
                    }
                    else if (countLostComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Lost;
                    }
                    else
                    {
                        if (countRejectedComponent == opportunity.Components.Count)
                        {
                            opportunity.Status = Status.Rejected;
                        }
                        else
                        {
                            opportunity.Status = Status.Open;
                        }
                    }

                    _context.Opportunities.Update(opportunity);
                    _context.Entry(opportunity).State = EntityState.Modified;

                    _context.Request.Update(request);
                    _context.Entry(request).State = EntityState.Modified;

                    _context.SaveChanges();

                    FormattableString body = $"<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>  <title></title>  <!--[if !mso]><!-- -->  <meta http-equiv='X-UA-Compatible' content='IE=edge'>  <!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style type='text/css'>  #outlook a {{ padding: 0; }}  .ReadMsgBody {{ width: 100%; }}  .ExternalClass {{ width: 100%; }}  .ExternalClass * {{ line-height:100%; }}  body {{ margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}  table, td {{ border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}  img {{ border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}  p {{ display: block; margin: 13px 0; }}</style><!--[if !mso]><!--><style type='text/css'>  @media only screen and (max-width:480px) {{    @-ms-viewport {{ width:320px; }}    @viewport {{ width:320px; }}  }}</style><!--<![endif]--><!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'>  .outlook-group-fix {{    width:100% !important;  }}</style><![endif]--><!--[if !mso]><!-->    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>    <style type='text/css'>        @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);    </style>  <!--<![endif]--><style type='text/css'>  @media only screen and (min-width:480px) {{    .mj-column-per-100 {{ width:100%!important; }}  }}</style></head><body style='background: #FFFFFF;'>    <div class='mj-container' style='background-color:#FFFFFF;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>        <tr>          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:9px 0px 9px 0px;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>        <tr>          <td style='vertical-align:top;width:600px;'>      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:top;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><p><span style='font-size:18px;'>Hi,<br><br>Component in <strong>{opportunity.Account.Name} </strong>has been {statusForEmail}.</span></p></div></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></div></body></html>";

                    List<User> CCs = new List<User>();
                    List<User> TOs = new List<User>();
                    TOs.Add(user);

                    _utils.SendPipelineNotif(TOs, CCs, "Component " + statusForEmail, FormattableString.Invariant(body));

                    return opportunity;
                }
                else if (request.RequestType == RequestType.EDIT && requestAction == RequestAction.DENIED)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == component.CreatedById);

                    var componentVersion = new ComponentVersion()
                    {
                        ComponentId = component.Id,
                        Added = now,
                        OpportunityId = component.OpportunityId,
                        Description = component.Description,
                        CategoryId = component.CategoryId,
                        AccountExecutiveId = component.AccountExecutiveId,
                        SolutionsArchitectId = component.SolutionsArchitectId,
                        StageId = component.StageId,
                        ProductId = component.ProductId,
                        Qty = component.Qty,
                        PricePerUnit = component.PricePerUnit,
                        CostPerUnit = component.CostPerUnit,
                        POC = component.Poc,
                        Status = component.Status,
                        Remarks = component.Remarks,
                        Modified = (DateTime)component.Modified,
                        ModifiedById = component.ModifiedById,
                        RequestId = component.RequestId,
                        VersionNumber = component.VersionNumber,
                    };

                    _context.ComponentVersions.Add(componentVersion);
                    _context.Entry(componentVersion).State = EntityState.Added;


                    request.ImplementedBy = component.CreatedById;
                    request.ImplementedOn = now;
                    request.RequestAction = requestAction;

                    //The following code is used to update the status of the opportunity base on the overall status of its components
                    int countOpenComponent = 0;
                    int countForApprovalComponent = 0;
                    int countRejectedComponent = 0;
                    int countLostComponent = 0;
                    int countClosedComponent = 0;

                    var statusForEmail = "";

                    foreach (var item in opportunity.Components)
                    {
                        if (item.RequestId == request.Id)
                        {
                            if (component.ComponentVersionId != null)
                            {
                                var prevComponent = _context.ComponentVersions.FirstOrDefault(c => c.Id == component.ComponentVersionId);

                                item.AccountExecutiveId = prevComponent.AccountExecutiveId;
                                item.CategoryId = prevComponent.CategoryId;
                                item.ComponentTypeId = prevComponent.ComponentTypeId;
                                item.CostPerUnit = prevComponent.CostPerUnit;
                                item.Description = prevComponent.Description;
                                item.Modified = prevComponent.Modified;
                                item.ModifiedById = prevComponent.ModifiedById;
                                item.OpportunityId = prevComponent.OpportunityId;
                                item.Poc = prevComponent.POC;
                                item.PricePerUnit = prevComponent.PricePerUnit;
                                item.ProductId = prevComponent.ProductId;
                                item.Qty = prevComponent.Qty;
                                item.Remarks = prevComponent.Remarks;
                                item.RequestId = prevComponent.RequestId;
                                item.SolutionsArchitectId = prevComponent.SolutionsArchitectId;
                                item.StageId = prevComponent.StageId;
                                item.TargetCloseDate = prevComponent.TargetCloseMonth;
                                item.ValidityDate = prevComponent.Validity;
                                item.VersionNumber = component.VersionNumber + 1;
                                item.Status = prevComponent.Status;

                                statusForEmail = prevComponent.Status.ToString().ToLower();
                            }
                            else
                            {
                                item.Status = status;
                                statusForEmail = status.ToString().ToLower();
                            }

                        }

                        if (item.Status == Status.Open)
                        {
                            countOpenComponent++;
                        }
                        else if (item.Status == Status.ForApproval)
                        {
                            countForApprovalComponent++;
                        }
                        else if (item.Status == Status.Rejected)
                        {
                            countRejectedComponent++;
                        }
                        else if (item.Status == Status.Closed)
                        {
                            countClosedComponent++;
                        }
                        else if (item.Status == Status.Lost)
                        {
                            countLostComponent++;
                        }
                    }

                    if (countForApprovalComponent != 0)
                    {
                        opportunity.Status = Status.ForApproval;
                    }
                    else if (countClosedComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Closed;
                    }
                    else if (countLostComponent == opportunity.Components.Count)
                    {
                        opportunity.Status = Status.Lost;
                    }
                    else
                    {
                        if (countRejectedComponent == opportunity.Components.Count)
                        {
                            opportunity.Status = Status.Rejected;
                        }
                        else
                        {
                            opportunity.Status = Status.Open;
                        }
                    }

                    _context.Opportunities.Update(opportunity);
                    _context.Entry(opportunity).State = EntityState.Modified;

                    _context.Request.Update(request);
                    _context.Entry(request).State = EntityState.Modified;

                    _context.SaveChanges();

                    FormattableString body = $"<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>  <title></title>  <!--[if !mso]><!-- -->  <meta http-equiv='X-UA-Compatible' content='IE=edge'>  <!--<![endif]--><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style type='text/css'>  #outlook a {{ padding: 0; }}  .ReadMsgBody {{ width: 100%; }}  .ExternalClass {{ width: 100%; }}  .ExternalClass * {{ line-height:100%; }}  body {{ margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}  table, td {{ border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}  img {{ border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }}  p {{ display: block; margin: 13px 0; }}</style><!--[if !mso]><!--><style type='text/css'>  @media only screen and (max-width:480px) {{    @-ms-viewport {{ width:320px; }}    @viewport {{ width:320px; }}  }}</style><!--<![endif]--><!--[if mso]><xml>  <o:OfficeDocumentSettings>    <o:AllowPNG/>    <o:PixelsPerInch>96</o:PixelsPerInch>  </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'>  .outlook-group-fix {{    width:100% !important;  }}</style><![endif]--><!--[if !mso]><!-->    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>    <style type='text/css'>        @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);    </style>  <!--<![endif]--><style type='text/css'>  @media only screen and (min-width:480px) {{    .mj-column-per-100 {{ width:100%!important; }}  }}</style></head><body style='background: #FFFFFF;'>    <div class='mj-container' style='background-color:#FFFFFF;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'>        <tr>          <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'>      <![endif]--><div style='margin:0px auto;max-width:600px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:9px 0px 9px 0px;'><!--[if mso | IE]>      <table role='presentation' border='0' cellpadding='0' cellspacing='0'>        <tr>          <td style='vertical-align:top;width:600px;'>      <![endif]--><div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'><table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:top;' width='100%' border='0'><tbody><tr><td style='word-wrap:break-word;font-size:0px;padding:0px 0px 0px 0px;' align='left'><div style='cursor:auto;color:#000000;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;'><p><span style='font-size:18px;'>Hi,<br><br>Component in <strong>{opportunity.Account.Name} </strong>has been {statusForEmail}.</span></p></div></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>      </td></tr></table>      <![endif]--></div></body></html>";

                    List<User> CCs = new List<User>();
                    List<User> TOs = new List<User>();
                    TOs.Add(user);

                    _utils.SendPipelineNotif(TOs, CCs, "Component " + statusForEmail, FormattableString.Invariant(body));

                    return opportunity;
                }
            }

            return null;
        }
    }
}
