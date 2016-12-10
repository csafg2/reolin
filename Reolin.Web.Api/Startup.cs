using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.DependecyRegistration;
using Reolin.Web.Api.Infra.ConfigExtensions;
using Reolin.Web.Api.Infra.Middlewares;

namespace Reolin.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddJwtDependencies();
            services.AddUserManager(Configuration.GetConnectionString("Default"));
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSqlLogger(Configuration["ConnectionStrings:Log"]);

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
            }

            app.UseExceptionHandler("/Error/SomeThingWentWrong");

            app.UseJwtValidation();
            app.UseMvcWithDefaultRoute();
        }
    }
}
