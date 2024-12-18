using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STC.API.Entities.CashReimbursement;
using STC.API.Entities.CashReimbursementEntity;
using STC.API.Models.CashReimbursement;
using STC.API.Services;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace STC.API.Controllers
{
    [Route("cashreimbursement")]
    public class CashReimbursementController : ControllerBase
    {
        private ICashReimbursementData _cashReimbursement;
        private IUserData _userData;
        private IHostingEnvironment _hosting;
        private IReimburseeData _reimbursee;
        private IConverter _converter;

        public CashReimbursementController(ICashReimbursementData cashReimbursement, IUserData userData, IReimburseeData reimburseeData, 
            IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            _cashReimbursement = cashReimbursement;
            _userData = userData;
            _hosting = hostingEnvironment;
            _reimbursee = reimburseeData;
            _converter = converter;
        }

        [HttpPost("expense")]
        public IActionResult AddExpense([FromBody] AddExpenseDto expenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var expense = _cashReimbursement.AddExpense(expenseDto);

            return Ok(expense);
        }

        [HttpPost("category")]
        public IActionResult AddExpenseCategory([FromBody] AddExpenseCategoryDto expenseCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var expenseCategory = _cashReimbursement.AddExpenseCategory(expenseCategoryDto);
            return Ok(expenseCategory);
        }

        [HttpGet("expensetypes")]
        public IActionResult GetExpenses()
        {
            var result = _cashReimbursement.GetExpenses();
            return Ok(result);
        }

        [HttpGet("today")]
        public IActionResult GetTodayReimbursement()
        {
            var result = _cashReimbursement.GetTodayReimbursement();
            return Ok(result);
        }

        [HttpGet("unprocessed/{createdBy}")]
        public IActionResult GetUnprocessedReimbursement(int createdBy)
        {
            var result = _cashReimbursement.GetUnprocessedReimbursement(createdBy);
            return Ok(result);
        }

        [HttpGet("processed")]
        public IActionResult GetProcessedReimbursement()
        {
            var result = _cashReimbursement.GetProcessedReimbursement();
            return Ok(result);
        }

        [HttpPost("userreimbursement")]
        public IActionResult AdduserReimbursement([FromBody] AddUserReimbursement userReimbursment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reimbursee = _reimbursee.GetReimbursee(userReimbursment.ReimburseeId);

            if (reimbursee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Reimbursee not exist");
            }

            if (reimbursee.BankAccountNumber == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User don't have bank account number");
            }

            var createdBy = _userData.GetUser(userReimbursment.CreatedById);

            if (createdBy == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User not exist");
            }

            var reimbursement = _cashReimbursement.AddReimbursement(userReimbursment, reimbursee, createdBy);



            return Ok(reimbursement);
        }

        [HttpGet("extract/{createdBy}")]
        public IActionResult Extract(int createdBy)
        {

            // Create a string array with the lines of text
            string[] lines = _cashReimbursement.ExtractReimbursements();

            // Set a variable to the Documents path.
            string docPath = _hosting.ContentRootPath;
            docPath = docPath + "\\cashreimbursement\\extract";

            bool exists = System.IO.Directory.Exists(docPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(docPath);

            var fileName =  "extract.txt";

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(docPath + "\\" + fileName);
            return File(fileBytes, "application/text", fileName);
        }

        [HttpGet("report/{createdBy}")]
        public IActionResult Report(int createdBy)
        {
            var batch = _cashReimbursement.GetUnprocessedReimbursement(createdBy);

            if (batch == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Batch not found");
            }


            string docPath = _hosting.ContentRootPath;
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            fileName = fileName + createdBy.ToString()+".pdf";

            docPath = docPath + "\\cashreimbursement\\report\\";

            bool exists = System.IO.Directory.Exists(docPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(docPath);

            docPath = docPath + fileName;

            if (System.IO.File.Exists(docPath))
            {
                // If file found, delete it    
                System.IO.File.Delete(docPath);
            }

            //var fs = new FileStream(docPath, FileMode.Create, FileAccess.Write);

            //IWorkbook workbook = new XSSFWorkbook();

            //var sheet3 = workbook.CreateSheet("Sheet1");


            List<string> headerExpenses = new List<string>();
            List<Reimbursee> Users = new List<Reimbursee>();

            headerExpenses.Add("Name");
            headerExpenses.Add("Account No.");


            List<string> headerExpensesToSort = new List<string>();


            foreach (var r in batch)
            {
                if (!Users.Contains(r.Reimbursee))
                {
                    Users.Add(r.Reimbursee);
                }

                foreach (var ue in r.UserExpenses)
                {
                    if (!headerExpensesToSort.Contains(ue.Expense.ExpenseCategory.Name))
                    {
                        headerExpensesToSort.Add(ue.Expense.ExpenseCategory.Name);
                    }
                }
            }

            headerExpensesToSort.Sort();
            headerExpenses.AddRange(headerExpensesToSort);
            
            headerExpenses.Add("Total");
            double[] headerExpensesTotal = new double[headerExpenses.Count-1];

            int rowCounter = 1;
            foreach (var user in Users.OrderBy(u => u.FirstName))
            {
                decimal rowTotal = 0;
                for (int i = 1; i < headerExpenses.Count - 1; i++)
                {
                    var category = headerExpenses[i];
                    decimal categoryTotal = 0;
                    foreach (var ur in batch.Where(u => u.ReimburseeId == user.Id))
                    {
                        foreach (var ure in ur.UserExpenses)
                        {
                            if (ure.Expense.ExpenseCategory.Name == category)
                            {
                                categoryTotal += ure.Amount;
                            }
                        }
                    }
                    rowTotal += categoryTotal;
                    headerExpensesTotal[i] += (double)categoryTotal;
                }
                rowCounter++;
            }

            double total = 0;
            for (int i = 1; i < headerExpensesTotal.Length; i++)
            {
                total += headerExpensesTotal[i];
            }

            var pdfHtml = new StringBuilder();
            pdfHtml.Append(@"
                <html>
    <head>
    </head>
    <body><br/>
        <table class='report'><thead><tr>
            ");

            for (int i = 0; i < headerExpenses.Count; i++)
            {
                pdfHtml.AppendFormat(@"<td>{0}</td>", headerExpenses[i].ToString());
            }
            pdfHtml.Append(@"</tr></thead><tbody>");

            rowCounter = 1;
            foreach (var user in Users.OrderBy(u => u.FirstName))
            {
                pdfHtml.Append(@"<tr>");            
                pdfHtml.AppendFormat(@"<td>{0}</td>", user.FirstName + " " + user.LastName);
                pdfHtml.AppendFormat(@"<td>{0}</td>", user.BankAccountNumber);


                decimal rowTotal = 0;
                //2 skip name acc no
                for (int i = 2; i < headerExpenses.Count - 1; i++)
                {
                    var category = headerExpenses[i];
                    decimal categoryTotal = 0;
                    foreach (var ur in batch.Where(u => u.ReimburseeId == user.Id))
                    {
                        foreach (var ure in ur.UserExpenses)
                        {
                            if (ure.Expense.ExpenseCategory.Name == category)
                            {
                                categoryTotal += ure.Amount;
                            }
                        }
                    }
                    rowTotal += categoryTotal;
                    pdfHtml.AppendFormat(@"<td>{0}</td>", String.Format("{0:#,0.00}", categoryTotal));

                }
                pdfHtml.AppendFormat(@"<td>{0}</td>", String.Format("{0:#,0.00}", rowTotal));
                rowCounter++;
                pdfHtml.Append(@"</tr>");
            }

            pdfHtml.Append(@"<tr>");

            for (int i = 0; i < headerExpenses.Count-1; i++)
            {
                //Skip the name column
                if (i == 0 || i == 1)
                {
                    pdfHtml.Append(@"<td></td>");
                }
                else
                {
                    pdfHtml.AppendFormat(@"<td>{0}</td>", String.Format("{0:#,0.00}", headerExpensesTotal[i]));
                }
            }
            pdfHtml.AppendFormat(@"<td>{0}</td>", String.Format("{0:#,0.00}", total));
            pdfHtml.Append(@"</tr>");


            pdfHtml.AppendFormat(@"</tbody></table><br/><p class='timestamp'>This report is generated on {0}</p></body></html>", TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")).ToString("dddd, dd MMMM yyyy hh:mm tt"));

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
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = _hosting.ContentRootPath+"\\css\\table.css" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Shellsoft Technology Corporation Cash Reimbursement Report" },
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

        [HttpPost("processed")]
        public IActionResult ProcessedReimbursement([FromBody] ProcessedDto processedDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _cashReimbursement.ProcessedReimbursement(processedDto);

            return Ok();
        }

        [HttpPut("userreimbursement")]
        public IActionResult EditUserReimbursement([FromBody] EditUserReimbursement userReimbursment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ur = _cashReimbursement.GetReimbursement(userReimbursment.Id);
            
            if (ur == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User reimbursement not exist");
            }

            var tmp = _cashReimbursement.EditReimbursement(ur, userReimbursment);
            var result = _cashReimbursement.GetReimbursementWithIncludes(tmp.Id);

            return Ok(result);
        }

        [HttpDelete("userreimbursement/{id}")]
        public IActionResult DeleteUserReimbursement(int id)
        {

            var r = _cashReimbursement.GetReimbursement(id);

            if (r == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User reimbursement not found");
            }

            _cashReimbursement.DeleteReimbursement(r);

            return Ok();
        }

        [HttpGet("processuserexpenses/{processBy}")]
        public IActionResult GetUserExpenses(int processBy)
        {
            var result = _cashReimbursement.GetProcessedUserExpense(processBy);
            return Ok(result);
        }
    }
}