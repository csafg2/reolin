using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Api.Infra.OptionsConfig;


namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class ConfigureUploadDirectory
    {
        public static IServiceCollection ConfigureDirectorySettings(this IServiceCollection source, IConfigurationRoot config)
        {
            return source.Configure<UploadDirectorySettings>
                (o => config.GetSection("UploadDirectorySettings").Bind(o));
        }

    }
}
