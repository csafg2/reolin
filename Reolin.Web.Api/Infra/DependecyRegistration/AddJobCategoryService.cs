#pragma warning disable CS1591
using Microsoft.Extensions.DependencyInjection;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddJobCategoryServiceEx
    {
        public static IServiceCollection AddJobCategoryService(this IServiceCollection source)
        {
            return source
                .AddTransient(typeof(IJobCategoryService), typeof(JobCategoryService));
        }
    }
}
