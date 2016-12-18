using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Reolin.Web.Security.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        public JwtSecurityToken CreateJwt(TokenProviderOptions options)
        {
            string sub = options.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub))
            {
                throw new ArgumentNullException("sub '(username)' claim can not be null");
            }
            

            DateTime now = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: options.Claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
        }

        public string JwtToString(JwtSecurityToken jwt)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string ProvideJwt(TokenProviderOptions options)
        {
            return new JwtSecurityTokenHandler().WriteToken(this.CreateJwt(options));
        }
    }
}