using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reolin.Web.Security.Jwt;
using System;
using System.Text;

namespace Reolin.Web.Api.Infra.Middlewares
{

    public static class JwtHelpers
    {
        public static IApplicationBuilder AddJwtValidation(this IApplicationBuilder source)
        {
            return source.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtManager.Configuration.SigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = JwtManager.Configuration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtManager.Configuration.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });

        }

        public static IApplicationBuilder AddJwtEndPoint(this IApplicationBuilder source)
        {
            var options = Options.Create(new TokenProviderOptions()
            {
                Audience = JwtManager.Configuration.Audience,
                Issuer = JwtManager.Configuration.Issuer,
                SigningCredentials = JwtManager.Configuration.SigningCredentials,
                Expiration = JwtManager.Configuration.Expiry
            });

            string path = "/Auth";

            return source.UseMiddleware<JwtProviderMiddleware>(options, path);
        }
    }
}