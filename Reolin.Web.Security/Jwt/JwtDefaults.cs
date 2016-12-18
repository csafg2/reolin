using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Reolin.Web.Security.Jwt
{
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
                return TimeSpan.FromMinutes(3);
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