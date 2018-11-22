using STC.API.Entities.ProductEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.AccountEntity
{
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        public string ContactDetails { get; set; }

        public int AccountIndustryId { get; set; }
        [ForeignKey("AccountIndustryId")]
        public AccountIndustry Industry { get; set; }

        public int TermsOfPayment { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public ICollection<AccountContact> AccountContacts { get; set; }
    
    }
}
