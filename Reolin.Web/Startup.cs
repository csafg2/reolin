using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Reolin.Web
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
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddDebug();
            
            // TODO: modify this into a separat extension method
            //StaticFileOptions options = new StaticFileOptions();
            //FileExtensionContentTypeProvider typeProvider = new FileExtensionContentTypeProvider();
            //if (!typeProvider.Mappings.ContainsKey(".woff2"))
            //{
            //    typeProvider.Mappings.Add(".woff2", "application/font-woff2");
            //}
            //options.ContentTypeProvider = typeProvider;
            app.UseStaticFiles();
            
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            //}
            //else
            //{
            // app.UseExceptionHandler("/Home/Error");
            //}


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
