using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Domain;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Security.Membership.Core;
using Reolin.Web.ViewModels;
using Reolin.Web.ViewModels.ViewModels.User;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Old pass is required")]
        public string OldPassword { get; set; }

        [Compare("ConfirmNewPassword")]
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public int Id { get; set; }
    }

    /// <summary>
    /// User related apis 
    /// </summary>
    [EnableCors("AllowAll")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        private readonly IUserSecurityManager _userSecurityManager;

#pragma warning disable CS1591
        public UserController(IUserService service, IUserSecurityManager userSecurityManager)
        {
            this._service = service;
            this._userSecurityManager = userSecurityManager;
        }
#pragma warning restore CS1591

        private IUserService UserService
        {
            get
            {
                return _service;
            }
        }


        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> ChangePassword(ChangePassword model)
        {
            await this._userSecurityManager.ChangePasswordAsync(model.Id, model.OldPassword, model.NewPassword);

            return Ok();
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
        /// <param name="id">the id of desired user</param>
        /// <returns></returns>
        [Route("/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> QueryProfiles(int id)
        {
            var profiles = await this.UserService.QueryProfilesAsync(id);
            
            return Ok(profiles);
        }


        /// <summary>
        /// add a comment to profile by userId
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddComment(CommentCreateModel model)
        {
            var userId = this.GetUserId();
            await this._service.AddCommentAsync(userId, model.ProfileId, model.Message);

            return Ok();
        }
    }
}
