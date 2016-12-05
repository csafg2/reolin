using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
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
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtDefaults.SigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = JwtDefaults.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtDefaults.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });

            
        }

        public static IApplicationBuilder UseJwtEndPoint(this IApplicationBuilder source)
        {
            var options = Options.Create(new TokenProviderOptions()
            {
                Audience = JwtDefaults.Audience,
                Issuer = JwtDefaults.Issuer,
                SigningCredentials = JwtDefaults.SigningCredentials,
                Expiration = JwtDefaults.Expiry
            });
            
            return source.UseMiddleware<JwtProviderMiddleware>(options, ConfigurationManager.TokenEndPoint);
        }
    }
}