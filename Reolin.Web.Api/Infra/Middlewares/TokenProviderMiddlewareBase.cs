using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Reolin.Web.Security.Jwt;
using System;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.Middlewares
{

    public class TokenProviderMiddlewareBase
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly string _path;

        public TokenProviderMiddlewareBase(RequestDelegate next, IOptions<TokenProviderOptions> options, string path)
        {
            _next = next;
            _options = options.Value;
            this._path = path;
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
                this.OnTokenCreating(context, _options);
                string jwt = new JwtProvider().ProvideJwt(_options);
                string response = JwtManager.CreateResponseString(jwt, this._options.Expiration);
                return WriteToken(context, response);
            }
            catch (Exception ex)
            {
                return WriteError(context, ex.Message);
            }
        }

        protected virtual void OnTokenCreating(HttpContext context, TokenProviderOptions options)
        {
            // allow sub class to incoporate
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
