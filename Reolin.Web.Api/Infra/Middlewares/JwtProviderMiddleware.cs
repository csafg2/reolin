using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Data.Domain;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                args.Cancel("both username and password are required.");
                return;
            }
            
            User user = await this.UserManager.GetLoginInfo(userName, password);
            if(user == null)
            {
                args.Cancel($"the user {userName} dose not exist.");
                return;
            }
            
            options.Claims = GetClaims(userName, user.Roles.Select(r => r.Name));
        }
        

        private List<Claim> GetClaims(string userName, IEnumerable<string> roles)
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
            foreach (var item in roles)
            {
                sb.Append($"{item},");
            }
            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}