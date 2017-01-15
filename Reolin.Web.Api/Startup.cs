using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.ConfigExtensions;
using Reolin.Web.Api.Infra.DependecyRegistration;
using Reolin.Web.Api.Infra.Middlewares;

namespace Reolin.Web.Api
{

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsWithDefaultConfig();

            services.AddJwtDependencies();
            services.AddJwtValidationRequirement(services.BuildServiceProvider());
            services.AddMemoryCache();
            services.AddLogging();
            services.AddUserManager(Configuration.GetConnectionString("Default"));
            services.AddMvcWithConfig();

            
            services.AddSwaggerAndConfigure();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler("/Error/SomeThingWentWrong");

            loggerFactory.AddSqlLogger(connectionString: Configuration["ConnectionStrings:Log"]);

            // comment this entire "if statement" in production
            if (env.IsDevelopment())
            {
                //app.UseStaticFiles();
                loggerFactory.AddDebug();
            }


            //app.UseDeveloperExceptionPage();

            app.UseJwtValidation();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}