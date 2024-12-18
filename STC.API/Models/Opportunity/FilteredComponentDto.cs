using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class FilteredComponentDto
    {
        public int Id { get; set; }
        public Entities.OpportunityEntity.Opportunity Opportunity { get; set; }
        public Entities.AccountEntity.Account Account { get; set; }
        public string Description { get; set; }
        public ComponentType ComponentType { get; set; }
        public Entities.UserEntity.User AccountExecutive { get; set; }
        public Stage Stage { get; set; }
        public Entities.ProductEntity.Product Product { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal PricePerUnit { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal CostPerUnit { get; set; }
        public Status Status { get; set; }
        public DateTime Modified { get; set; }
        public string Remarks { get; set; }
    }
}
