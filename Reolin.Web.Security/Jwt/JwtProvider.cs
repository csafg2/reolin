using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Newtonsoft.Json;

namespace Reolin.Web.Security.Jwt
{

    public class JwtProvider : IJwtTokenProvider
    {
        /// <summary>
        /// gets claims that token provider will ultimately set on jwt
        /// </summary>
        public IEnumerable<Claim> Claims { get; private set; }

        /// <summary>
        ///  gets options that are set for this jwt generator
        /// </summary>
        public TokenProviderOptions Options { get; private set; }
        
        private string CreateResponse()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    access_token = this.ProvideToken(),
                    expires_in = (int)Options.Expiration.TotalSeconds
                });
        }

        private string ProvideToken()
        {
            string sub = this.Options.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub))
            {
                throw new ArgumentException("sub (username) claim can not be null");
            }
         
            var now = DateTime.UtcNow;

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: this.Claims,
                notBefore: now,
                expires: now.Add(Options.Expiration),
                signingCredentials: Options.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Creates jwt encoded string based on options
        /// </summary>
        /// <param name="options"></param>
        /// <returns>fully signed token</returns>
        public string ProvideToken(TokenProviderOptions options)
        {
            if (!options.IsValid())
            {
                throw new InvalidOperationException("options object is not valid");
            }

            this.Options = options;
            return this.CreateResponse();
        }
    }
}