﻿using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using static Reolin.Web.Security.Jwt.JwtConstantsLookup;

namespace Reolin.Web.Api.Helpers
{
    public static class HttpRequestExtensions
    {
    

        public static string BuildString(this JwtSecurityToken source)
        {
            return new JwtSecurityTokenHandler().WriteToken(source);
        }

        public static JwtSecurityToken GetRequestToken(this HttpRequest source)
        {
            
            string jwt = source.Headers
                           .FirstOrDefault(h => h.Key == HEADER_KEY)
                                   .Value;
            if (string.IsNullOrEmpty(jwt))
            {
                return null;
            }

            jwt = jwt.Replace(TOKEN_SCHEME, string.Empty);

            return new JwtSecurityToken(jwt);
        }
    }
}
