using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Api.Infra.IO;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddFileServiceExtension
    {

        public static IServiceCollection AddFileService(this IServiceCollection source, string basePath)
        {
            return source
                .AddTransient(typeof(IFileService), isp => new FileService(basePath, new TwoCharDirectoryProvider()));
        }
    }
}
