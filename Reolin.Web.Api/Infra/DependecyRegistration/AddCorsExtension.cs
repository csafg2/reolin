using Microsoft.Extensions.DependencyInjection;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddCorsExtension
    {
        public static IServiceCollection AddCorsWithDefaultConfig(this IServiceCollection source)
        {
            return source.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                        builder.AllowCredentials();
                    });
            });
        }
    }
}
