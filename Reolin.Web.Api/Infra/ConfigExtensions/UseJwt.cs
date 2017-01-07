using Microsoft.AspNetCore.Builder;
using Reolin.Web.Security.Jwt;

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