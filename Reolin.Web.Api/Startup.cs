using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.Middlewares;
using Reolin.Web.Api.Infra.DependecyRegistration;

namespace Reolin.Web.Api
{

    class Requirement : AuthorizationHandler<Requirement>, IAuthorizationRequirement
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            Requirement requirement)
        {
            return Task.FromResult(0);
        }
    }

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
            services.AddJwtManager();
            services.AddUserManager();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseJwtValidation();
            app.UseJwtEndPoint();
            app.UseMvcWithDefaultRoute();
            
            app.UseMvc();
        }
    }
}
