using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Reolin.Web.Api.Infra.AuthorizationRequirments;
using Reolin.Web.Security.Jwt;
using System;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddJwtManagerExtension
    {
        public static IServiceCollection AddJwtValidationRequirement(this IServiceCollection services, IServiceProvider provider)
        {
            IJwtManager jwtManager = provider.GetService<IJwtManager>();
            return services.AddAuthorization(o =>
            {
                o.AddPolicy(ValidTokenRequirment.Name,
                    b => b.Requirements.Add(new ValidTokenRequirment(jwtManager)));

                o.DefaultPolicy = o.GetPolicy(ValidTokenRequirment.Name);
            });
        }

        public static IServiceCollection AddJwtDependencies(this IServiceCollection source)
        {
            return source.AddTransient(typeof(IJwtProvider), typeof(JwtProvider))
                            .AddTransient(typeof(IOptions<TokenProviderOptions>), p => TokenOptions)
                            .AddSingleton<IJwtStore>(new InMemoryJwtStore())
                            .AddTransient(typeof(IJwtManager), typeof(JwtManager));
        }

        private static IOptions<TokenProviderOptions> TokenOptions
        {
            get
            {
                return Options.Create(new TokenProviderOptions()
                {
                    Audience = JwtConfigs.Audience,
                    Issuer = JwtConfigs.Issuer,
                    SigningCredentials = JwtConfigs.SigningCredentials,
                    Expiration = JwtConfigs.Expiry
                });
            }
        }
    }
}
