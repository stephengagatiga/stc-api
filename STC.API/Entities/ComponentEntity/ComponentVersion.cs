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
        [ForeignKey("ComponentId")]
        public Component Component { get; set; }

        public DateTime Added { get; set; }

        public int OpportunityId { get; set; }
        [ForeignKey("OpportunityId")]
        public Opportunity Opportunity { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int ComponentTypeId { get; set; }
        [ForeignKey("ComponentTypeId")]
        public ComponentType ComponentType { get; set; }

        public int AccountExecutiveId { get; set; }
        [ForeignKey("AccountExecutiveId")]
        public User AccountExecutive { get; set; }

        public int SolutionsArchitectId { get; set; }
        [ForeignKey("SolutionsArchitectId")]
        public User SolutionsArchitect { get; set; }

        public DateTime TargetCloseMonth { get; set; }

        public int StageId { get; set; }
        [ForeignKey("StageId")]
        public Stage Stage { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "decimal(12, 4)")]
        public decimal PricePerUnit { get; set; }

        [Column(TypeName = "decimal(12, 4)")]
        public decimal CostPerUnit { get; set; }

        public bool POC { get; set; }
        public DateTime Validity { get; set; }
        public Status Status { get; set; }
        public string Remarks { get; set; }
        public DateTime Created { get; set; }

        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public int ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public User ModifiedBy { get; set; }

        public int VersionNumber { get; set; }

    }
}
