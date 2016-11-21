using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Reolin.Web.Security.Jwt
{
    public class TokenProviderOptions
    {
        public TokenProviderOptions()
        {

        }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public List<Claim> Claims { get; set; }

        internal bool IsValid()
        {
            //TODO: check required options parameters..
            return true;
        }
    }
}