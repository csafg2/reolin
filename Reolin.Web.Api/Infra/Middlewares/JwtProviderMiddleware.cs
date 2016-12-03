using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Data.Domain;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Reolin.Web.Api.Infra.Middlewares
{
    public class JwtProviderMiddleware : TokenProviderMiddlewareBase
    {
        public JwtProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, string path, IUserSecurityManager userManager)
            : base(next, options, path, userManager)
        {
        }

        protected async override Task OnTokenCreating(HttpContext context, TokenProviderOptions options, TokenArgs args)
        {
            string userName = context.Request.Form["userName"];
            string password = context.Request.Form["password"];

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                args.Cnaceled = true;
                args.Reason = "Username or password can not be empty";
                return;
            }
            
            User user = await this.UserManager.GetLoginInfo(userName, password);
            if(user == null)
            {
                args.Cnaceled = true;
                args.Reason = "user dose not exist";
                return;
            }
            
            string[] roles = user.Roles.Select(r => r.Name).ToArray();

            options.Claims = GetClaims(userName, roles).ToList();
        }
        

        private IEnumerable<Claim> GetClaims(string userName, string[] roles)
        {
            return new Claim[]
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim("roles", GetRoleString(roles), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                   };
        }

        private string GetRoleString(string[] roles)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in roles)
            {
                sb.Append($"{item},");
            }
            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}