using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddJwtManagerExtension
    {
        public static IServiceCollection AddJwtManager(this IServiceCollection source)
        {
            return source.AddTransient(typeof(IJwtProvider), typeof(JwtProvider))
                            .AddSingleton(typeof(IJwtStore), typeof(InMemoryJwtStore))
                            .AddTransient(typeof(IJWTManager), typeof(JwtManager));
        }
    }
}
