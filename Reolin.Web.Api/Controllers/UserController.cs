using Microsoft.AspNetCore.Mvc;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    /// <summary>
    /// User related apis 
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserService _service;

#pragma warning disable CS1591
        public UserController(IUserService service)
        {
            this._service = service;
        }
#pragma warning restore CS1591

        private IUserService UserService
        {
            get
            {
                return _service;
            }
        }

        /// <summary>
        /// set the first name and lastname of specified user, the user id has to be present in reqest header
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok if Succeeded</returns>
        public async Task<IActionResult> SetFirstNameLastName(SetFirstNameLastNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await this.UserService.SetUserInfo(this.GetUserId(), model.FirstName, model.LastName);
            return Ok();
        }


        /// <summary>
        /// Query profile entries that are attached to a userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> QueryProfiles(int userId)
        {
            return Ok(await this.UserService.QueryProfiles(userId));
            
        }
    }
}
