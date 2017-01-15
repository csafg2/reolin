using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Reolin.Web.Security.Jwt;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.Middlewares
{
    internal static class JwtHelpers
    {
        public static IApplicationBuilder UseJwtValidation(this IApplicationBuilder source)
        {
            return source.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = JwtConfigs.ValidationParameters,
                Events = new JwtBearerEvents()
                {
                    OnTokenValidated = ctx =>
                    {
                        return Task.FromResult(0);
                    },
                    OnMessageReceived = ctx =>
                     {
                         return Task.FromResult(0);
                     },
                    OnChallenge = ctx =>
                    {
                        return Task.FromResult(0);
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}