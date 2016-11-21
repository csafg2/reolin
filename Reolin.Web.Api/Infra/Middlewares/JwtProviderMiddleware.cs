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
                throw new InvalidOperationException("username and password is required");
            }

            options.Claims =  new List<Claim>();
            foreach (var item in GetClaims(userName))
            {
                options.Claims.Add(item);
            }
            //TODO: add claims like role and username
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