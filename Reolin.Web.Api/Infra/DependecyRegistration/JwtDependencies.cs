using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddJwtManagerExtension
    {
        public static IServiceCollection AddJwtDependencies(this IServiceCollection source)
        {
            return source.AddTransient(typeof(IJwtProvider), typeof(JwtProvider))
                            .AddTransient(typeof(IOptions<TokenProviderOptions>), p =>
                            {
                                return Options.Create(new TokenProviderOptions()
                                {
                                    Audience = JwtDefaults.Audience,
                                    Issuer = JwtDefaults.Issuer,
                                    SigningCredentials = JwtDefaults.SigningCredentials,
                                    Expiration = JwtDefaults.Expiry
                                });
                            })
                            .AddSingleton(typeof(IJwtStore), typeof(InMemoryJwtStore))
                            .AddTransient(typeof(IJWTManager), typeof(JwtManager));
        }
    }
}
