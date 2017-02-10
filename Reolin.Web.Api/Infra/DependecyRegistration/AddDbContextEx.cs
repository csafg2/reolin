#pragma warning disable CS1591
using Microsoft.Extensions.DependencyInjection;
using Reolin.Data;
using System;

namespace Reolin.Web.Api.Infra.DependecyRegistration
{
    public static class AddDbContextEx
    {
        public static IServiceCollection AddDbContext(this IServiceCollection source, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            return source.AddScoped(typeof(DataContext), isp => new DataContext(connectionString));
        }
    }
}
