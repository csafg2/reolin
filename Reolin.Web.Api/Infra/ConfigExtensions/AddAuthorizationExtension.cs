using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Security.Jwt;
using System;
namespace Reolin.Web.Api.Infra.ConfigExtensions
{
    public static class AddAuthorizationExtensions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IServiceProvider provider)
        {
            IJwtManager jwtManager = (IJwtManager)provider.GetService(typeof(IJwtManager));
            return services.AddAuthorization(o =>
            {
                o.AddPolicy("ValidJwt", b => b.Requirements.Add(new ValidTokenRequirment(jwtManager, provider)));
                o.DefaultPolicy = o.GetPolicy("ValidJwt");
            });
        }
    }
}
