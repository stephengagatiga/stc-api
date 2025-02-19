﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.PO
{
    public class NewPOPendingDraft
    {
        public string ReferenceNumber { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public int? ContactPersonId { get; set; }
        public string ContactPersonName { get; set; }
        public string CustomerName { get; set; }
        public DateTime? EstimatedArrival { get; set; }
        public string Currency { get; set; }
        [Range(minimum: 0, maximum: 100)]
        public decimal Discount { get; set; }
        public int? ApproverId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public string InternalNote { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string RequestorEmail { get; set; }
        public string POPendingItemsJsonString { get; set; }
    }
}
