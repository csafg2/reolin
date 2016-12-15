using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.ConfigExtensions;
using Reolin.Web.Api.Infra.DependecyRegistration;
using Reolin.Web.Api.Infra.Middlewares;
using System.Linq;
using System.Security.Claims;
using Reolin.Web.Security.Jwt;
using Reolin.Web;

namespace Reolin.Web.Api
{

    public class ValidTokenRequirment : AuthorizationHandler<ValidTokenRequirment>, IAuthorizationRequirement
    {
        private IJwtManager _jwtManager;
        private IServiceProvider _provider;

        public ValidTokenRequirment(IJwtManager manager, IServiceProvider provider)
        {
            _jwtManager = manager;
            this._provider = provider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenRequirment requirement)
        {
            Claim tokenId = context.User.Claims.FirstOrDefault(c => c.Type == "jti");
            Claim userNameClaim = context.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (new[] { tokenId, userNameClaim }.Any(o => o == null))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            var p = this._provider;
            if (!_jwtManager.ValidateToken(userNameClaim.Value, tokenId.Value))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(this);
            }

            return Task.FromResult(0);
        }
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
        services.AddJwtDependencies();
        services.AddJwtAuthorization(services.BuildServiceProvider());
        services.AddMemoryCache();
        services.AddLogging();
        services.AddUserManager(Configuration.GetConnectionString("Default"));
        services.AddMvc();
    }


    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        // uncomment this one in production
        app.UseExceptionHandler("/Error/SomeThingWentWrong");

        loggerFactory.AddSqlLogger(connectionString: Configuration["ConnectionStrings:Log"]);

        // comment this entire "if statement" in production
        if (env.IsDevelopment())
        {
            loggerFactory.AddDebug();
        }

        //app.UseDeveloperExceptionPage();

        app.UseJwtValidation();
        app.UseMvcWithDefaultRoute();
    }
}