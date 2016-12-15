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
                            .AddTransient(typeof(IOptions<TokenProviderOptions>),
                                p => Options.Create(new TokenProviderOptions()
                                {
                                    Audience = JwtConfigs.Audience,
                                    Issuer = JwtConfigs.Issuer,
                                    SigningCredentials = JwtConfigs.SigningCredentials,
                                    Expiration = JwtConfigs.Expiry
                                }))
                            .AddSingleton<IJwtStore>(new InMemoryJwtStore())
                            .AddTransient(typeof(IJwtManager), typeof(JwtManager));
        }
    }
}
