using System;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Reolin.Web.Security.Jwt
{
    public static class JwtManager
    {
        public  class Configuration
        {
            public static TimeSpan Expiry
            {
                get
                {
                    return TimeSpan.FromMinutes(10);
                }
            }

            public static string Audience
            {
                get
                {
                    return "ExampleAudience";
                }
            }

            public static string Issuer
            {
                get
                {
                    return "ExampleIssuer";
                }
            }

            public const string SecretKey = "SDFLKSJFLSDJFJSDLFJKSDLFJSDLSDFLKJSDFHSDL";
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