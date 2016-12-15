using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Reolin.Web.Security.Jwt;
using System;

namespace Reolin.Web.Api.Infra.Middlewares
{
    public static class JwtHelpers
    {
        public static IApplicationBuilder UseJwtValidation(this IApplicationBuilder source)
        {
            return source.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = JwtConfigs.ValidationParameters
            });
            
        }
    }
}