using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Reolin.Web.Api.Helpers;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Reolin.Web.Security.Jwt.JwtConstantsLookup;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
#pragma warning disable CS1591
    public class AccountController : BaseController
    {
        private readonly IUserSecurityManager _userManager;
        private readonly IOptions<TokenProviderOptions> _tokenOptionsWrapper;
        private readonly IJwtManager _jwtManager;

        public AccountController(IUserSecurityManager userManager, IOptions<TokenProviderOptions> options, IJwtManager jwtManager)
#pragma warning restore CS1591
        {
            this._userManager = userManager;
            this._jwtManager = jwtManager;
            this._tokenOptionsWrapper = options;
        }

        #region Properties
        private TokenProviderOptions Options
        {
            get
            {
                return _tokenOptionsWrapper.Value;
            }
        }

        private IUserSecurityManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        private IJwtManager JwtManager
        {
            get
            {
                return _jwtManager;
            }
        }
        #endregion


        /// <summary>
        /// Exchanges an Expired Token with a new Token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult ExchangeToken()
        {
            var token = Request.GetRequestToken();
            if (token == null)
            {
                return BadRequest();
            }

            try
            {
                return Json(new
                {
                    newToken = this.JwtManager.ExchangeToken(token),
                    expiresIn = Options.Expiration
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        /// <summary>
        /// Logout the user, the jwt MUST BE valid and present in the requst header
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult Logout()
        {
            JwtSecurityToken requestToken = this.HttpContext.Request.GetRequestToken();

            this.JwtManager.InvalidateToken(requestToken.Claims.GetUsernameClaim().Value, requestToken.Id);

            return Ok();
        }


        /// <summary>
        /// Create a new user for specified information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            try
            {
                await this.UserManager.CreateAsync(model.UserName, model.Password, model.Email);
                return Ok(new { model.Email, model.UserName });
            }
            catch (IdentityException ex)
            {
                this.ModelState.AddModelError("identificationError", ex.Message);
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Login to the app by providing user information
        /// </summary>
        /// <param name="model"></param>
        /// <returns>a json object that contains the jwt </returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IdentityResult result = (await this.UserManager.GetLoginInfo(model.UserName, model.Password));
            if (!result.Succeeded)
            {
                return WriteError(result);
            }

            this.Options.Claims = GetPerUserClaims(result.User.UserName, result.User.Id, result.User.Roles.Select(r => r.Name));

            return Ok(new
            {
                accessToken = this.JwtManager.IssueJwt(this.Options),
                expiresIn = Options.Expiration
            });
        }

        private IActionResult WriteError(IdentityResult result)
        {
            switch (result.Error)
            {
                case IdentityResultErrors.EmptyOrUnknown:
                    return Error("Something went wrong.");

                case IdentityResultErrors.UserNotFound:
                    return Error(HttpStatusCode.NotFound, "Specified User could not be found");

                case IdentityResultErrors.InvalidPassowrd:
                    return Error(HttpStatusCode.BadRequest, "Invalid password");

                default:
                    return RedirectToAction("SomeThingWentWrong", "ErrorController");
            }
        }

        private List<Claim> GetPerUserClaims(string userName, int userId, IEnumerable<string> roles)
        {
            return new List<Claim>()
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim(Role_CLAIM_TYPE, GetRoleString(roles), ROLE_VALUE_TYPE),
                        new Claim(ID_CLAIM_TYPE, userId.ToString(), ClaimValueTypes.Integer32)
                   };
        }


        /// <summary>
        /// convert string[] into some thing like this: "admin,normalUser, ..., ..."
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        private string GetRoleString(IEnumerable<string> roles)
        {
            StringBuilder sb = new StringBuilder();
            roles.ForEach(r => sb.Append($"{r},"));
            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}
