using Microsoft.AspNetCore.Authorization;
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
    public class AccountController : BaseController
    {
        private readonly IUserSecurityManager _userManager;
        private readonly IOptions<TokenProviderOptions> _tokenOptionsWrapper;
        private readonly IJwtManager _jwtManager;

        public AccountController(IUserSecurityManager userManager, IOptions<TokenProviderOptions> options, IJwtManager jwtManager)
        {
            this._userManager = userManager;
            this._jwtManager = jwtManager;
            this._tokenOptionsWrapper = options;
        }

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

        [HttpPost]
        public IActionResult ExchangeToken()
        {
            var token = Request.GetRequestToken();
            if (token == null)
            {
                return BadRequest();
            }

            string userName = token.Claims.GetUsernameClaim().Value;
            if (!this.JwtManager.VerifyToken(token.BuildString(), JwtConfigs.ValidationParameters)
                ||
                !this.JwtManager.ValidateToken(userName, token.Id))
            {
                return BadRequest();
            }

            this.JwtManager.InvalidateToken(userName, token.Id);

            this.Options.Claims = token.Claims.ToList();

            return Json(new
            {
                access_token = this.JwtManager.IssueJwt(this.Options),
                expires_in = Options.Expiration
            });
        }

        [Authorize]
        public IActionResult Logout()
        {
            JwtSecurityToken requestToken = this.HttpContext.Request.GetRequestToken();

            if (requestToken == null)
            {
                return BadRequest();
            }

            this.JwtManager.InvalidateToken(requestToken.Claims.GetUsernameClaim().Value, requestToken.Id);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            try
            {
                await this.UserManager.CreateAsync(model.UserName, model.Password, model.Email);
                return Ok(new { model.Email, model.UserName });
            }
            catch (Exception ex) when (ex is IdentityException)
            {
                this.ModelState.AddModelError("identificationError", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result = (await this.UserManager.GetLoginInfo(model.UserName, model.Password));
            if (!result.Succeeded)
            {
                return WriteError(result);
            }

            this.Options.Claims = GetPerUserClaims(result.User.UserName, result.User.Id, result.User.Roles.Select(r => r.Name));

            return Ok(new
            {
                access_token = this.JwtManager.IssueJwt(this.Options),
                expires_in = Options.Expiration
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
                    return BadRequest(result.Exception.Message);
            }
        }

        private List<Claim> GetPerUserClaims(string userName, int userId, IEnumerable<string> roles)
        {
            const string roleClaimName = "roles";
            return new List<Claim>()
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim(roleClaimName, GetRoleString(roles), ROLE_VALUE_TYPE),
                        new Claim(ID_CLAIM_TYPE, userId.ToString(), ClaimValueTypes.Integer32)
                   };
        }

        private string GetRoleString(IEnumerable<string> roles)
        {
            StringBuilder sb = new StringBuilder();
            roles.ForEach(r => sb.Append($"{r},"));
            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}
