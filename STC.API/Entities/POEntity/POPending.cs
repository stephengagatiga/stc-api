using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.POEntity
{
    public class POPending
    {
        [Key]
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public int? ContactPersonId { get; set; }
        public string ContactPersonName { get; set; }
        public string CustomerName { get; set; }
        public DateTime? EstimatedArrival { get; set; }
        public string EstimatedArrivalString { get; set; }
        public string Currency { get; set; }
        public int? ApproverId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public string ApproverJobTitle { get; set; }

        [Range(minimum: 0, maximum: 100)]
        public decimal Discount { get; set; }
        [Range(minimum: -1_000_000_000, maximum: 1_000_000_000)]
        public decimal Total { get; set; }
        public string InternalNote { get; set; }
        public string Remarks { get; set; }
        public POStatus Status { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int? CreatedById { get; set; }
        [Required]
        public string RequestorName { get; set; }
        [Required]
        public string RequestorEmail { get; set; }
        [Required]
        public string CreatedByName { get; set; }
        public ICollection<POPendingItem> POPendingItems { get; set; }
        public ICollection<POAuditTrail> POAuditTrails { get; set; }
        public string POPendingItemsJsonString { get; set; }
        public Guid Guid { get; set; }

        //This hold how many line breaks are there in PO items and remarks
        //The purpose of this is to disrtibue the PO items in pages if there are more
        //and to keep the signatory at bottom when not in the page 1\
        //the format of this is: 10,30,20 
        //where the last value is the remarks line breaks count
        public string TextLineBreakCount { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public DateTime? CancelledOn { get; set; }
        //This is to check if PO has been approved in the past
        //so you can notify the approver about the updated PO
        public bool HasBeenApproved { get; set; }
        public int OrderNumber  { get; set; }
    }
}
