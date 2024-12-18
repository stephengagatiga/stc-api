using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.POEntity;

namespace STC.API.Services
{
    public class SqlPOGuidStatusData : IPOGuidStatusData
    {
        STCDbContext _context;
        public SqlPOGuidStatusData(STCDbContext context)
        {
            _context = context;
        }

        public POGuidStatus GetPOGuid(Guid guid)
        {
             return _context.POGuidStatus.Where(p => p.POGuid == guid).FirstOrDefault();
        }

        public ICollection<POGuidStatusAttachment> GetPOGuidStatusAttachments(int pOGuidId)
        {
            return _context.POGuidStatusAttachments.Where(p => p.POGuidStatusId == pOGuidId).ToList();
        }

        public POGuidStatus UpdatePOStatus(POGuidStatus pOGuidStatus, POStatus pOStatus)
        {
            pOGuidStatus.POStatus = pOStatus;
            pOGuidStatus.ModifiedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
            _context.POGuidStatus.Update(pOGuidStatus);
            _context.Entry(pOGuidStatus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

            return pOGuidStatus;
        }
    }
}
