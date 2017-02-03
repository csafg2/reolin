using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Api.Infra.IO;
using System.IO;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddFileServiceExtension
    {
        public static IServiceCollection AddFileService(this IServiceCollection source)
        {
            return source
                .AddTransient(typeof(IDirectoryProvider), typeof(TwoCharDirectoryProvider))
                .AddTransient(typeof(IFileService), typeof(FileService));
        }
    }
}
