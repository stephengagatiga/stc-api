﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.PO
{
    public class NewPOPending
    {
        [Required]
        public string ReferenceNumber { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string SupplierAddress { get; set; }
        [Required]
        public int ContactPersonId { get; set; }
        [Required]
        public string ContactPersonName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime? EstimatedArrival { get; set; }
        [Required]
        public string Currency { get; set; }
        [Range(minimum: 0, maximum: 100)]
        public decimal Discount { get; set; }
        [Required]
        public int ApproverId { get; set; }
        [Required]
        public string ApproverName { get; set; }
        [Required]
        public string ApproverEmail { get; set; }
        public string ApproverJobTitle { get; set; }

        public string InternalNote { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int CreatedById { get; set; }
        [Required]
        public string CreatedByName { get; set; }
        [Required]
        public string RequestorEmail { get; set; }
        [Required]
        public ICollection<NewPOPendingItem> POPendingItems { get; set; }
    }

}
