using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

namespace Reolin.Web.Security.Jwt
{

    public class TokenProviderOptions
    {
        public string SecretKey { get; set; }
        public string Path { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public Claim[] Claims { get; set; }

        internal bool IsValid()
        {
            return true;
        }
    }
}