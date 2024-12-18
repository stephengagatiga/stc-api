using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.POEntity;
using STC.API.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class SqlPOPendingData : IPOPendingData
    {
        private STCDbContext _context;

        public SqlPOPendingData(STCDbContext context)
        {
            _context = context;
        }

        public POPending AddPO(NewPOPending pOPending)
        {
            var item = new POPending()
            {
                ReferenceNumber = pOPending.ReferenceNumber,

                SupplierId = pOPending.SupplierId,
                SupplierName = pOPending.SupplierName,
                SupplierAddress = pOPending.SupplierAddress,

                ContactPersonId = pOPending.ContactPersonId,
                ContactPersonName = pOPending.ContactPersonName,

                CustomerName = pOPending.CustomerName,

                EstimatedArrival = pOPending.EstimatedArrival,
                EstimatedArrivalString = pOPending.EstimatedArrival != null ? ((DateTime)(pOPending.EstimatedArrival)).ToString("dddd, dd MMMM yyyy") : "",

                Currency = pOPending.Currency,

                ApproverName = pOPending.ApproverName,
                ApproverId = pOPending.ApproverId,
                ApproverEmail = pOPending.ApproverEmail,

                InternalNote = pOPending.InternalNote,
                Remarks = pOPending.Remarks,

                Discount = pOPending.Discount,
                CreatedByName = pOPending.CreatedByName,
                CreatedById = pOPending.CreatedById,
                RequestorEmail = pOPending.RequestorEmail,
                CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")),
                Status = POStatus.Pending,
                Guid = Guid.NewGuid()
            };

            item.POPendingItems = new List<POPendingItem>();
            foreach (var i in pOPending.POPendingItems)
            {
                var pOItems = new POPendingItem()
                {
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Total = i.Price * i.Quantity
                };

                item.Total = item.Total + pOItems.Total;
                item.POPendingItems.Add(pOItems);
            }

            _context.POPendings.Add(item);
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return item;
        }

        public POPending AddPODraft(NewPOPendingDraft pOPendingDraft)
        {
            var item = new POPending()
            {
                ReferenceNumber = pOPendingDraft.ReferenceNumber,

                SupplierId = pOPendingDraft.SupplierId,
                SupplierName = pOPendingDraft.SupplierName,
                SupplierAddress = pOPendingDraft.SupplierAddress,

                ContactPersonId = pOPendingDraft.ContactPersonId,
                ContactPersonName = pOPendingDraft.ContactPersonName,

                CustomerName = pOPendingDraft.CustomerName,

                EstimatedArrival = pOPendingDraft.EstimatedArrival,
                EstimatedArrivalString = pOPendingDraft.EstimatedArrival != null ? ((DateTime)(pOPendingDraft.EstimatedArrival)).ToString("dddd, dd MMMM yyyy") : "",

                Currency = pOPendingDraft.Currency,

                ApproverName = pOPendingDraft.ApproverName,
                ApproverId = pOPendingDraft.ApproverId,
                ApproverEmail = pOPendingDraft.ApproverEmail,

                InternalNote = pOPendingDraft.InternalNote,
                Remarks = pOPendingDraft.Remarks,

                Discount = pOPendingDraft.Discount,
                CreatedByName = pOPendingDraft.CreatedByName,
                CreatedById = pOPendingDraft.CreatedById,
                RequestorEmail = pOPendingDraft.RequestorEmail,
                CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time")),
                Status = POStatus.Draft,
                POPendingItemsJsonString = pOPendingDraft.POPendingItemsJsonString
            };

 

            _context.POPendings.Add(item);
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return item;
        }

        public POPending GetPO(int poId)
        {
            var po = _context.POPendings.Where(p => p.Id == poId)
                                            .Include(p => p.POPendingItems)
                                            .FirstOrDefault();

            return po;
        }

        public POPending GetPOByGuid(Guid guid)
        {
            var po = _context.POPendings.FirstOrDefault(p => p.Guid == guid);
            return po;
        }

        public ICollection<POPending> GetPOs()
        {
            var result = _context.POPendings
                                    .OrderByDescending(p => p.Id)
                                    .Include(i => i.POPendingItems)
                                    .ToList();
            return result;
        }

        public POPending UpdatePO(EditPOPending newPOPending, POPending oldPOPending)
        {
            oldPOPending.ApproverEmail = newPOPending.ApproverEmail;
            oldPOPending.ApproverId = newPOPending.ApproverId;
            oldPOPending.ApproverName = newPOPending.ApproverName;
            oldPOPending.ContactPersonId = newPOPending.ContactPersonId;
            oldPOPending.ContactPersonName = newPOPending.ContactPersonName;
            oldPOPending.Currency = newPOPending.Currency;
            oldPOPending.CustomerName = newPOPending.CustomerName;
            oldPOPending.Discount = newPOPending.Discount;
            oldPOPending.EstimatedArrival = newPOPending.EstimatedArrival;
            oldPOPending.EstimatedArrivalString = newPOPending.EstimatedArrival == null ? "" : ((DateTime)newPOPending.EstimatedArrival).ToString("MM/dd/yyyy");
            oldPOPending.Guid = Guid.NewGuid();
            oldPOPending.InternalNote = newPOPending.InternalNote;
            oldPOPending.POPendingItemsJsonString = newPOPending.POPendingItemsJsonString;
            oldPOPending.ReferenceNumber = newPOPending.ReferenceNumber;
            oldPOPending.Remarks = newPOPending.Remarks;
            oldPOPending.RequestorEmail = newPOPending.RequestorEmail;
            oldPOPending.Status = newPOPending.Status;
            oldPOPending.SupplierAddress = newPOPending.SupplierAddress;
            oldPOPending.SupplierId = newPOPending.SupplierId;
            oldPOPending.SupplierName = newPOPending.SupplierName;

            var oldPOPendingItems = _context.POPendingItems.Where(p => p.POPendingId == oldPOPending.Id).ToList();
            foreach (var item in oldPOPendingItems)
            {
                _context.POPendingItems.Remove(item);
                _context.Entry(item).State = EntityState.Deleted;
            }

            oldPOPending.POPendingItems = new List<POPendingItem>();
            decimal total = 0;
            foreach (var item in newPOPending.POPendingItems)
            {
                decimal amount = item.Price * item.Quantity;
                oldPOPending.POPendingItems.Add(new POPendingItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = amount
                });
                total += amount;
            }

            oldPOPending.Total = total;

            _context.POPendings.Update(oldPOPending);
            _context.Entry(oldPOPending).State = EntityState.Modified;

            _context.SaveChanges();

            return oldPOPending;
        }

        public POPending UpdatePO(EditPOPendingDraft newPOPending, POPending oldPOPending)
        {
            oldPOPending.ApproverEmail = newPOPending.ApproverEmail;
            oldPOPending.ApproverId = newPOPending.ApproverId;
            oldPOPending.ApproverName = newPOPending.ApproverName;
            oldPOPending.ContactPersonId = newPOPending.ContactPersonId;
            oldPOPending.ContactPersonName = newPOPending.ContactPersonName;
            oldPOPending.Currency = newPOPending.Currency;
            oldPOPending.CustomerName = newPOPending.CustomerName;
            oldPOPending.Discount = newPOPending.Discount;
            oldPOPending.EstimatedArrival = newPOPending.EstimatedArrival;
            oldPOPending.EstimatedArrivalString = newPOPending.EstimatedArrival == null ? "" : ((DateTime)newPOPending.EstimatedArrival).ToString("MM/dd/yyyy");
            oldPOPending.Guid = Guid.NewGuid();
            oldPOPending.InternalNote = newPOPending.InternalNote;
            oldPOPending.POPendingItemsJsonString = newPOPending.POPendingItemsJsonString;
            oldPOPending.ReferenceNumber = newPOPending.ReferenceNumber;
            oldPOPending.Remarks = newPOPending.Remarks;
            oldPOPending.RequestorEmail = newPOPending.RequestorEmail;
            oldPOPending.Status = newPOPending.Status;
            oldPOPending.SupplierAddress = newPOPending.SupplierAddress;
            oldPOPending.SupplierId = newPOPending.SupplierId;
            oldPOPending.SupplierName = newPOPending.SupplierName;

            var oldPOPendingItems = _context.POPendingItems.Where(p => p.POPendingId == oldPOPending.Id).ToList();
            foreach (var item in oldPOPendingItems)
            {
                _context.POPendingItems.Remove(item);
                _context.Entry(item).State = EntityState.Deleted;
            }

            oldPOPending.POPendingItems = new List<POPendingItem>();
            decimal total = 0;
            foreach (var item in newPOPending.POPendingItems)
            {
                decimal amount = item.Price * item.Quantity;
                _context.POPendingItems.Add(new POPendingItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = amount,
                    POPendingId = oldPOPending.Id
                });
                //oldPOPending.POPendingItems.Add(new POPendingItem()
                //{
                //    Name = item.Name,
                //    Price = item.Price,
                //    Quantity = item.Quantity,
                //    Total = amount
                //});
                total += amount;
            }

            oldPOPending.Total = total;

            _context.POPendings.Update(oldPOPending);
            _context.Entry(oldPOPending).State = EntityState.Modified;

            _context.SaveChanges();

            return oldPOPending;
        }

        public void UpdatePOStatus(POPending pOPending, POStatus pOStatus)
        {
            pOPending.Status = pOStatus;
            _context.POPendings.Update(pOPending);
            _context.Entry(pOPending).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
