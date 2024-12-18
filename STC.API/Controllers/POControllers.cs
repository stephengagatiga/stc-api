using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STC.API.Entities.POEntity;
using STC.API.Models.PO;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("po")]
    public class POControllers : Controller
    {
        private IPOGuidStatusData _pOGuidStatusData;
        private IUtils _utils;
        private IHostingEnvironment _hostingEnvironment;
        private IConverter _converter;

        public POControllers(IPOGuidStatusData pOGuidStatusData, IUtils utils)
        {
            _pOGuidStatusData = pOGuidStatusData;
            _utils = utils;
        }

        [HttpGet("approve/{guid}")]
        public IActionResult ApprovePOStatus(Guid guid)
        {
            var po = _pOGuidStatusData.GetPOGuid(guid);
            if (po == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO not found");
            }

            if (po.POStatus != POStatus.Pending)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO was " + po.POStatus.ToString() + " on " + po.ModifiedOn.ToString("MMMM dd, yyyy"));
            }

            var attachments = _pOGuidStatusData.GetPOGuidStatusAttachments(po.Id);
            _pOGuidStatusData.UpdatePOStatus(po, POStatus.Approved);
            _utils.SendPOToDistirubtionList(po, attachments, POStatus.Approved);
            return Ok("Approved, please close this page.");
        }

        [HttpGet("reject/{guid}")]
        public IActionResult RejectPOStatus(Guid guid)
        {
            var po = _pOGuidStatusData.GetPOGuid(guid);
            if (po == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO not found");
            }

            if (po.POStatus != POStatus.Pending)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO was " + po.POStatus.ToString() + " on " + po.ModifiedOn.ToString("MMMM dd, yyyy"));
            }

            _pOGuidStatusData.UpdatePOStatus(po, POStatus.Rejected);
            _utils.SendPORejectNotification(po);
            return Ok("Rejected, please close this page.");
        }

        [HttpGet("cancel/approve/{guid}")]
        public IActionResult ApproveCancelPOStatus(Guid guid)
        {
            var po = _pOGuidStatusData.GetPOGuid(guid);
            if (po == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO not found");
            }

            if (po.POStatus != POStatus.Pending)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO was " + po.POStatus.ToString() + " on " + po.ModifiedOn.ToString("MMMM dd, yyyy"));
            }

            var attachments = _pOGuidStatusData.GetPOGuidStatusAttachments(po.Id);
            _pOGuidStatusData.UpdatePOStatus(po, POStatus.Cancelled);
            _utils.SendPOToDistirubtionList(po, attachments, POStatus.Cancelled);
            return Ok("Cancellation approved, please close this page.");
        }

        [HttpGet("cancel/reject/{guid}")]
        public IActionResult RejectCancelPOStatus(Guid guid)
        {
            var po = _pOGuidStatusData.GetPOGuid(guid);
            if (po == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO not found");
            }

            if (po.POStatus != POStatus.Pending)
            {
                return StatusCode(StatusCodes.Status404NotFound, "PO was " + po.POStatus.ToString() + " on " + po.ModifiedOn.ToString("MMMM dd, yyyy"));
            }

            _pOGuidStatusData.UpdatePOStatus(po, POStatus.CancellationRejected);
            return Ok("Cancellation rejected, please close this page.");
        }

    }
}
