using Microsoft.Extensions.DependencyInjection;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddEntityServices
    {
        public static IServiceCollection AddProfileService(this IServiceCollection source)
        {
            return source.AddTransient(typeof(IProfileService), typeof(ProfileService));
        }
    }
}
