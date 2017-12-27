using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Reolin.Web.Api.Infra.Filters;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddMvcExtension
    {
        public static IMvcBuilder AddMvcWithConfig(this IServiceCollection services)
        {
            return services.AddMvc()
                .AddJsonOptions(o => 
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });//o => o.Filters.Add(new RequireValidModelAttribute()));
        }
    }
}
