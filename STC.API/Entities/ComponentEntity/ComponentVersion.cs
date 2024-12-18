using STC.API.Entities.OpportunityEntity;
using STC.API.Entities.ProductEntity;
using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.ComponentEntity
{
    public class ComponentVersion
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public DateTime Added { get; set; }
        public int OpportunityId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int? ComponentTypeId { get; set; }
        public int AccountExecutiveId { get; set; }
        public int SolutionsArchitectId { get; set; }
        public DateTime? TargetCloseMonth { get; set; }
        public int StageId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal PricePerUnit { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal CostPerUnit { get; set; }
        public bool POC { get; set; }
        public DateTime? Validity { get; set; }
        public Status Status { get; set; }
        public string Remarks { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedById { get; set; }
        public Guid RequestId { get; set; }
        public int VersionNumber { get; set; }
    }
}
