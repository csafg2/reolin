#pragma warning disable CS1591
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
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
            System.Console.WriteLine(env.ContentRootPath);
            SqlServerTypes.Utilities.LoadNativeAssemblies(env.ContentRootPath);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Environment = env;
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.UseJwtValidation();
            services.ConfigureDirectorySettings(Configuration);
            services.AddDbContext(Configuration.GetConnectionString("Default"));
            services.AddJobCategoryService();
            //services.AddProfileService();
            services.AddEntityServices();
            services.AddCorsWithDefaultConfig();
            services.AddFileService();
            services.AddJwtDependencies();
            services.AddJwtValidationRequirement(services.BuildServiceProvider());
            services.AddMemoryCache();
            services.AddLogging();
            services.AddUserManager(Configuration.GetConnectionString("Default"));
            System.Console.WriteLine(Configuration.GetConnectionString("Default"));
            services.AddMvcWithConfig();
            services.AddSwaggerAndConfigure();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var provider = new FileExtensionContentTypeProvider();

            // Add new mappings
            provider.Mappings["."] = "text/plain";
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });


            loggerFactory.UseSqlLogger(connectionString: Configuration["ConnectionStrings:Log"]);
            app.UseDeveloperExceptionPage();
            // comment this entire "if statement" in production
            //if (env.IsDevelopment())
            //{
            //    loggerFactory.AddDebug();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error/SomeThingWentWrong");
            //}

            //app.UseDeveloperExceptionPage();

            //app.UseJwtValidation();
            //app.UseJwtBearerAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}