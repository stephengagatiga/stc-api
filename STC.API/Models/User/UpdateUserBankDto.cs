using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.User
{
    public class UpdateUserBankDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string BankAccountNo { get; set; }
    }
}
