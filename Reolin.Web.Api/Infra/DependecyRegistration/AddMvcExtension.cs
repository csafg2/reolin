using Microsoft.Extensions.DependencyInjection;
using Reolin.Web.Api.Infra.filters;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddMvcExtension
    {
        public static IMvcBuilder AddMvcWithConfig(this IServiceCollection services)
        {
            //TODO: enable default model validation in production
            return services.AddMvc();// o => o.Filters.Add(new RequireValidModelAttribute()));
        }
    }
}
