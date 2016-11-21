using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Reolin.Web.Security.Jwt
{
    public class JwtProvider : IJwtTokenProvider
    {
        public string ProvideJwt(TokenProviderOptions options)
        {
            string sub = options.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub))
            {
                throw new ArgumentException("sub (username) claim can not be null");
            }
         
            var now = DateTime.UtcNow;

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: options.Claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}