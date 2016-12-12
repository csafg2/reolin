using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        public IUserService UserService
        {
            get
            {
                return _service;
            }
        }


        // TODO: complete this method
        public async Task<IActionResult> SetFirstNameLastName(SetFirstNameLastNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await this.UserService.SetUserInfo(this.UserId, model.FirstName, model.LastName);
            return Ok();
        }
    }
}
