using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STC.API.Models.CashReimbursement;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("reimbursees")]

    public class ReimburseesController : Controller
    {
        private IReimburseeData _reimburseeData;
        private IHostingEnvironment _hostingEnvironment;
        private IConverter _converter;

        public ReimburseesController(IReimburseeData reimburseeData, IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            _reimburseeData = reimburseeData;
            _hostingEnvironment = hostingEnvironment;
            _converter = converter;
        }

        [HttpGet]
        public IActionResult GetReimbursess()
        {
            var result = _reimburseeData.GetReimbursees();
            return Ok(result);
        }

        [HttpGet("report")]
        public IActionResult GetReimbursessReport()
        {
            var result = _reimburseeData.GetReimbursees();

            string docPath = _hostingEnvironment.ContentRootPath;
            string fileName = "reimbursees.pdf";

            docPath = docPath + "\\reimbursess\\";

            bool exists = System.IO.Directory.Exists(docPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(docPath);

            docPath = docPath + fileName;

            if (System.IO.File.Exists(docPath))
            {
                // If file found, delete it    
                System.IO.File.Delete(docPath);
            }


            var pdfHtml = new StringBuilder();
            pdfHtml.Append(@"
                <html>
                <head>
                </head>
                <body>
                <br/>
                    <table class='reimbursees'>
                        <thead>
                            <tr>
                                <td>Name</td>
                                <td>Account Number</td>
                            </tr>
                        </thead><tbody>            
                    ");

            var reimbursees = _reimburseeData.GetReimbursees();

            foreach(var r in reimbursees)
            {
                pdfHtml.AppendFormat(@"<tr><td>{0} {1}</td><td>{2}</td></tr>", r.FirstName, r.LastName, r.BankAccountNumber);
            }

            pdfHtml.AppendFormat(@"<tr><td colspan='2'>Rows: {0}</td></tr></tbody></table><br/><p class='timestamp'>This report is generated on {1}</p></body></html>", reimbursees.Count, DateTime.Now.ToString("dddd, dd MMMM yyyy H:mm tt"));

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = docPath
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = pdfHtml.ToString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = _hostingEnvironment.ContentRootPath + "\\css\\table.css" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Shellsoft Technology Corporation Reimbursees" },
                FooterSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);


            byte[] fileBytes = System.IO.File.ReadAllBytes(docPath);
            return File(fileBytes, "application/pdf", fileName);
        }

        [HttpPost]
        public IActionResult AddReimbursee([FromBody] AddReimburseeDto reimburseeDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid request");
            }

            var r = _reimburseeData.CheckIfReimburseeExist(reimburseeDto);
            if (r != null)
            {
                if (r.FirstName.ToUpper() == reimburseeDto.FirstName.ToUpper())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Name already exist");
                }

                if (r.BankAccountNumber == reimburseeDto.BankAccountNumber)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bank account number exist");
                }
            }

            var reimbursee = _reimburseeData.AddReimbursee(reimburseeDto);

            return Ok(reimbursee);
        }
    }
}
