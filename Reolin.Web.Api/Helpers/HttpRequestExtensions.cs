﻿using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Reolin.Web.Api.Helpers
{
    public static class HttpRequestExtensions
    {
        const string TOKEN_SCHEME = "bearer ";
        const string HEADER_KEY = "Authorization";

        public static JwtSecurityToken GetRequestToken(this HttpRequest source)
        {
            string jwt = source.Headers
                            .First(h => h.Key == HEADER_KEY)
                                   .Value.ToString()
                                       .Replace(TOKEN_SCHEME, string.Empty);

            return new JwtSecurityToken(jwt);
        }
    }
}
