using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Entities.AccountEntity;

namespace STC.API.Models.Account
{
    public class AccountSuccessDto
    {
        public Entities.AccountEntity.Account Account { get; set; }
        public Entities.RequestEntity.Request Token { get; set; }

    }
}
