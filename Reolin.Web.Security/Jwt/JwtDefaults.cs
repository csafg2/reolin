using System;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Reolin.Web.Security.Jwt
{
    public static class JwtConstantsLookup
    {
        public const string TOKEN_SCHEME = "bearer ";
        public const string HEADER_KEY = "Authorization";
        public const string ROLE_VALUE_TYPE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public const string INT_VALUE_TYPE = "http://www.w3.org/2001/XMLSchema#integer";
        public const string ID_CLAIM_TYPE = "Id";
        public const string USERNAME_CLAIM_TYPE = "sub";
    }


    public static class JwtConfigs
    {
        
        public static TokenValidationParameters ValidationParameters
        {
            get
            {
                return new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }
        }

            public static TimeSpan Expiry
            {
                get
                {
                    return TimeSpan.FromMinutes(60);
                }
            }

            public static string Audience
            {
                get
                {
                    return "ApiClients";
                }
            }

            public static string Issuer
            {
                get
                {
                    return "Self";
                }
            }

            public const string SecretKey = "QWERASDZXCVFRTGBNHYUJMKIUOKLPO";
            public static SymmetricSecurityKey SigningKey
            {
                get
                {
                    return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
                }
            }

            public static SigningCredentials SigningCredentials
            {
                get
                {
                    return new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
                }
            }


        public static string CreateResponseString(string jwt, TimeSpan expiry)
        {
            return JsonConvert.SerializeObject(
                new
                {
                    access_token = jwt,
                    expires_in = expiry
                });
        }
    }
}