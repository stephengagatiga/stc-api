using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class OldComponentDto
    {
        public string CategoryName { get; set; }
        public string ComponentTypeName { get; set; }
        public string ProductName { get; set; }
        public string StageName { get; set; }
        public string TargetCloseDateString { get; set; }
        public string ValidityDateString { get; set; }
        public string AccountExecutiveName { get; set; }
        public string SolutionsArchitectName { get; set; }
        public decimal CostPerUnit { get; set; }
        public string Description { get; set; }
        public bool Poc { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Qty { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }

    }
}
