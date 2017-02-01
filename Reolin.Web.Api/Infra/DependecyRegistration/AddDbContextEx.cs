#pragma warning disable CS1591
using Microsoft.Extensions.DependencyInjection;
using Reolin.Data;
using System;
using System.Data.Entity;

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

            return source.AddTransient(typeof(DataContext), sp => new DataContext(connectionString));
        }
    }
}
