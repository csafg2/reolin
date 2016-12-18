using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Reolin.Web.Security.Jwt
{
    /// <summary>
    /// this class holds options for for generating a JWT
    /// </summary>
    public class TokenProviderOptions
    {
        /// <summary>
        /// Issuer field associated with the resulting jwt
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience field associated with the resulting jwt
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// jwt life time
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// Signing Credentials (includes the Secret Key and encrypting algorithm)
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// a list of claims associated with the JWT wich will be placed in jwt payload
        /// </summary>
        public List<Claim> Claims { get; set; }
    }
}