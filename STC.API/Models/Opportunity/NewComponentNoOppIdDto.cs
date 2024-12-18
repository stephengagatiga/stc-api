using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class NewComponentNoOppIdDto
    {
        [Required]
        public int AccountExecutiveId { get; set; }
        public string AccountExecutiveName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Nullable<int> ComponentTypeId { get; set; }
        public string ComponentTypeName { get; set; }
        public decimal CostPerUnit { get; set; }
        [Required]
        public int CreatedById { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ModifiedById { get; set; }
        public bool Poc { get; set; }
        public decimal PricePerUnit { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public int SolutionsArchitectId { get; set; }
        public string SolutionsArchitectName { get; set; }
        [Required]
        public int StageId { get; set; }
        public string StageName { get; set; }
        public Status Status { get; set; }
        public string TargetCloseDate { get; set; }
        public string TargetCloseDateString { get; set; }
        public string ValidityDate { get; set; }
        public string ValidityDateString { get; set; }
    }
}
