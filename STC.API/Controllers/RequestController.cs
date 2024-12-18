using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STC.API.Entities.RequestEntity;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("token")]
    public class RequestController : Controller
    {
        private IRequestData _tokenData;
        private IUtils _utils;

        public RequestController(IRequestData tokenData, IUtils utils)
        {
            _tokenData = tokenData;
            _utils = utils;
        }

        [HttpGet("{tokenId}")]
        public IActionResult ApproveToken(Guid tokenId)
        {
            var token = _tokenData.GetRequest(tokenId);
            if (token == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Something went wrong, We couldn't find this request");
            }

            if (token.RequestAction == RequestAction.PENDING)
            {
                var obj = _tokenData.Approve(token);
                if (obj != null)
                {
                    return Ok(obj);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something wen't wrong in the server");
                }
            }  else if (token.RequestAction == RequestAction.APPROVED || token.RequestAction == RequestAction.DENIED)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "This request has been " + token.RequestAction.ToString());
            }


            return BadRequest();

        }

        [HttpGet("reject/{tokenId}")]
        public IActionResult RejectToken(Guid tokenId)
        {
            var token = _tokenData.GetRequest(tokenId);
            if (token == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Something went wrong, We couldn't find this request");
            }

            if (token.RequestAction == RequestAction.PENDING)
            {
                var obj = _tokenData.Reject(token);
                if (obj != null)
                {
                    return Ok(obj);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something wen't wrong in the server");
                }
            }
            else if (token.RequestAction == RequestAction.APPROVED || token.RequestAction == RequestAction.DENIED)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "This request has been " + token.RequestAction.ToString());
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Something wen't wrong in the server");
        }

        [HttpGet("email/approve/{tokenId}")]
        public IActionResult EmailApprove(Guid tokenId)
        {
            var token = _tokenData.GetRequest(tokenId);
            if (token == null)
            {
                return NotFound();
            }

            if (token.RequestAction == RequestAction.PENDING)
            {
                _tokenData.Approve(token);
                return View();
            }


            return BadRequest();

        }

    }
}
