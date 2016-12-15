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
                    IssuerSigningKey = JwtConfigs.SigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = JwtConfigs.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtConfigs.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });
            
        }

        [Obsolete]
        public static IApplicationBuilder UseJwtEndPoint(this IApplicationBuilder source)
        {
            var options = Options.Create(new TokenProviderOptions()
            {
                Audience = JwtConfigs.Audience,
                Issuer = JwtConfigs.Issuer,
                SigningCredentials = JwtConfigs.SigningCredentials,
                Expiration = JwtConfigs.Expiry
            });
            
            return source.UseMiddleware<JwtProviderMiddleware>(options, ConfigurationManager.TokenEndPoint);
        }
    }
}