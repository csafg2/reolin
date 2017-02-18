using Microsoft.Extensions.DependencyInjection;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    internal static class AddEntitySEx
    {
        public static IServiceCollection AddProfileService(this IServiceCollection source)
        {
            return source.AddTransient(typeof(IProfileService), typeof(ProfileService));
        }

        public static IServiceCollection AddEntityServices(this IServiceCollection source)
        {
            return AddProfileService(source)
                .AddTransient(typeof(IImageCategoryService), typeof(ImageCategoryService))
                .AddTransient(typeof(ISuggestionService), typeof(SuggestionService));
        }
    }
}
