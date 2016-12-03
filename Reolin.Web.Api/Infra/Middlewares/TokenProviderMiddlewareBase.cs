using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership.Core;
using System;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.Middlewares
{
    public class TokenProviderMiddlewareBase
    {
        protected class TokenArgs
        {
            public bool Cnaceled { get; set; }
            public string Reason { get; set; }
        }

        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly string _path;
        private readonly IUserSecurityManager _userManager;

        public TokenProviderMiddlewareBase(RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            string path,
            IUserSecurityManager userManager)
        {
            this._next = next;
            this._options = options.Value;
            this._path = path;
            this._userManager = userManager;
        }
        
        protected IUserSecurityManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(this._path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }
            
            try
            {
                TokenArgs args = new TokenArgs();
                this.OnTokenCreating(context, _options, args);

                if (args.Cnaceled == true)
                {
                    return WriteError(context, args.Reason);
                }
            }
            catch(Exception ex)
            {
                return WriteError(context, ex.Message);
            }
            
            
            string jwt = new JwtProvider().ProvideJwt(_options);
            string response = JwtManager.CreateResponseString(jwt, this._options.Expiration);
            return WriteToken(context, response);
        }

        protected virtual Task OnTokenCreating(HttpContext context, TokenProviderOptions _options, TokenArgs args)
        {
            return Task.FromResult(0);
        }
        
        const string Json_MimeType = "application/json";
        private async Task WriteError(HttpContext context, string message)
        {
            await context.Response.WriteAsync(message);
        }

        private async Task WriteToken(HttpContext context, string jwtResponse)
        {
            context.Response.ContentType = Json_MimeType;
            await context.Response.WriteAsync(jwtResponse);
        }
    }
}
