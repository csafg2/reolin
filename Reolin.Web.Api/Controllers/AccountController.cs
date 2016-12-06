using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserSecurityManager _userManager;
        private readonly IOptions<TokenProviderOptions> _tokenOptionsWrapper;
        private readonly IJWTManager _jwtManager;

        public AccountController(IUserSecurityManager userManager, IOptions<TokenProviderOptions> options, IJWTManager jwtManager)
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

        private IJWTManager JwtManager
        {
            get
            {
                return _jwtManager;
            }
        }

        [HttpPost]
        [AllowAnonymous]
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
            catch (Exception ex) when (ex is IdentityException)
            {
                this.ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                // TODO: Log exception here
                return BadRequest();
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

            this.Options.Claims = GetPerUserClaims(model.UserName, result.User.Roles.Select(r => r.Name));

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
                    return NotFound("Something went wrong.");

                case IdentityResultErrors.UserNotFound:
                    return NotFound(result.Message);

                case IdentityResultErrors.InvalidPassowrd:
                    return BadRequest("Password is invalid.");

                default:
                    return BadRequest(result.Exception.Message);
            }
        }

        private List<Claim> GetPerUserClaims(string userName, IEnumerable<string> roles)
        {
            const string roleClaimName = "roles";
            return new List<Claim>()
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim(roleClaimName, GetRoleString(roles), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
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
