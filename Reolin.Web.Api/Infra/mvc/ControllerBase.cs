#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Security.Jwt;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Reolin.Web.Api.Infra.mvc
{

    public abstract class BaseController : Controller
    {
        /// <summary>
        /// extracts the user if from the request token.
        /// </summary>
        /// <returns>the id which has been found in request</returns>
        protected int GetUserId()
        {
            Claim idClaim = User.Claims.Where(c => c.Type == JwtConstantsLookup.ID_CLAIM_TYPE).FirstOrDefault();

            if (idClaim == null)
            {
                throw new Exception("userId claim could not be found");
            }

            return int.Parse(idClaim.Value);
        }

        protected IActionResult Error(Exception ex)
        {
            return this.Error(ex.Message);
        }

        protected IActionResult Error(HttpStatusCode code, string message)
        {
            // wrap message into an object in order to be properly json serializable
            return this.ApiResult(code, new { Message = message });
        }

        protected IActionResult Error(string message)
        {
            return this.ApiResult(HttpStatusCode.InternalServerError, new { Message = message });
        }

        protected IActionResult ApiResult(HttpStatusCode code, object data)
        {
            return new ApiResult(code, data);
        }
    }
}