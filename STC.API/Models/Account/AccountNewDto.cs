using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Account
{
    public class AccountNewDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        public string ContactDetails { get; set; }

        [Required]
        public int AccountIndustryId { get; set; }

        public int TermsOfPayment { get; set; }

        public string NotifToEmail { get; set; }
        public string NotiifToName { get; set; }

    }
}
