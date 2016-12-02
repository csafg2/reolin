using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Domain;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        protected async override Task OnTokenCreating(HttpContext context, TokenProviderOptions options, bool cancel)
        {
            string userName = context.Request.Form["userName"];
            string password = context.Request.Form["password"];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("username and password are required");
            }
            User currentUser = await GetUser(userName);
            if (currentUser == null)
            {
                // write error to response
                cancel = true;
                return;
            }

            if((currentUser = await VaildateUserIdentity(userName, password)) == null)
            {
                // write error to response
                cancel = true;
                return;
            }
            
            options.Claims = new List<Claim>();
            foreach (var item in GetClaims(userName, null))
            {
                options.Claims.Add(item);
            }
            
            //TODO: add claims like role and username
        }

        private Task<User> GetUser(string userName)
        {
            
            throw new NotImplementedException();
        }

        private Task<User> VaildateUserIdentity(string userName, string password)
        {
            throw new NotImplementedException();
            // hashpassword
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