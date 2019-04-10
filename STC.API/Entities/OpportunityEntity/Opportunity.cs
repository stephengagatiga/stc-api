using STC.API.Entities.AccountEntity;
using STC.API.Entities.UserEntity;
using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.OpportunityEntity
{
    public class Opportunity
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public string BigDealCode { get; set; }
        public DateTime Created { get; set; }

        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public int ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public User ModifiedBy { get; set; }

        public Status Status { get; set; }
        public ICollection<ComponentEntity.Component> Components { get; set; }
    }
}
