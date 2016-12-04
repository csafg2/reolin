using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserSecurityManager _userManager;

        public AccountController(IUserSecurityManager userManager)
        {
            this._userManager = userManager;
        }

        private IUserSecurityManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            try
            {
                await this.UserManager.CreateAsync(model.UserName, model.Password, model.Email);
                return Ok(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("error", ex.Message);
                return BadRequest(this.ModelState);
            }
        }
    }
}
