using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Reolin.Web.Api.Infra.IO;
using System.IO;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddFileServiceExtension
    {
        public static IServiceCollection AddFileService(this IServiceCollection source, string basePath)
        {
            basePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, basePath);
            IFileService instance = new FileService(basePath, new TwoCharDirectoryProvider());
            return source.AddTransient(typeof(IFileService), isp => instance);
        }
    }
}
