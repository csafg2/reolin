using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Api.Infra.IO;
using System.IO;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddFileServiceExtension
    {
        public static IServiceCollection AddFileService(this IServiceCollection source, string basePath, IHostingEnvironment env)
        {
            return source.AddTransient(typeof(IFileService), isp =>
                new FileService(Path.Combine(env.WebRootPath, basePath), new TwoCharDirectoryProvider()));
        }
    }
}
