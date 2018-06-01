using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Infra.Middlewares
{
    internal static class JwtHelpers
    {
        public static IServiceCollection UseJwtValidation(this IServiceCollection source)
        {
            source.AddAuthentication(o => 
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => 
            {
                o.TokenValidationParameters = JwtConfigs.ValidationParameters;
            });

            return source;
        }
    }
}