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
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly string _path;
        private readonly IUserSecurityManager _userManager;
        private readonly IJWTManager _jwtManager;

        public TokenProviderMiddlewareBase(RequestDelegate next, 
                                            IOptions<TokenProviderOptions> options,
                                            string path,
                                            IUserSecurityManager userManager,
                                            IJWTManager manager)
        {
            this._next = next;
            this._options = options.Value;
            this._path = path;
            this._userManager = userManager;
            this._jwtManager = manager;
        }
        
        protected IUserSecurityManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        private IJWTManager JwtManager
        {
            get
            {
                return _jwtManager;
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

                if (args.Canceled)
                {
                    return WriteError(context, args.Reason);
                }
            }
            catch(Exception ex)
            {
                return WriteError(context, ex.Message);
            }

            string jwt = this._jwtManager.IssueJwt(this._options);
            string response = JwtDefaults.CreateResponseString(jwt, this._options.Expiration);
            return WriteToken(context, response);
        }

        protected virtual Task OnTokenCreating(HttpContext context, TokenProviderOptions _options, TokenArgs args)
        {
            // do nothing, just allow sub class to hook in.
            return Task.FromResult(0);
        }
        
        private async Task WriteError(HttpContext context, string message)
        {
            await context.Response.WriteAsync(message);
        }

        private async Task WriteToken(HttpContext context, string jwtResponse)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jwtResponse);
        }

        protected class TokenArgs
        {
            public bool Canceled { get; private set; }
            public string Reason { get; set; }

            public void Cancel(string reason)
            {
                this.Canceled = true;
                this.Reason = reason;
            }
        }

    }
}
