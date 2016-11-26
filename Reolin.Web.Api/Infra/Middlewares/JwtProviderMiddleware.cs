using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Web.Security.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Reolin.Web.Api.Infra.Middlewares
{
    public class JwtProviderMiddleware : TokenProviderMiddlewareBase
    {
        public JwtProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, string path)
            : base(next, options, path)
        {
        }


        protected override void OnTokenCreating(HttpContext context, TokenProviderOptions options)
        {
            string userName = context.Request.Form["userName"];
            string password = context.Request.Form["password"];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("username and password are required");
            }
            VaildateUserIdentity(userName, password);
            options.Claims = new List<Claim>();
            foreach (var item in GetClaims(userName))
            {
                options.Claims.Add(item);
            }
            
            //TODO: add claims like role and username
        }

        private void VaildateUserIdentity(string userName, string password)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Claim> GetClaims(string userName)
        {
            return new Claim[]
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim("roles", "admin", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                   };
        }
    }
}