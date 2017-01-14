using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    /// <summary>
    /// Add Swagger SwashBuckle Service to service collection
    /// </summary>
    public static class AddSwaggerExtension
    {
        /// <summary>
        /// adds swagger
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerAndConfigure(this IServiceCollection source)
        {
            source.AddSwaggerGen();

            source.ConfigureSwaggerGen(options =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Reolin.Web.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });

            return source;
        }
    }
}
