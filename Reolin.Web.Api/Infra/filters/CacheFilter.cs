using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Reolin.Web.Api.Infra.filters
{
    public class CacheFilter : ActionFilterAttribute
    {
        private readonly IMemoryCache _cache;

        public CacheFilter(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public string Key { get; set; }
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }

        private IMemoryCache Cache
        {
            get
            {
                return _cache;
            }
        }

        private object _tempKey;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            KeyValuePair<string, object> arg = context.ActionArguments.FirstOrDefault(k => k.Key == this.Key.ToLower());
            if (string.IsNullOrEmpty(arg.Key))
            {
                return;
            }

            this._tempKey = arg.Value.ToString();
            object cachedResult = null;

            if (this.Cache.TryGetValue(arg.Value.ToString(), out cachedResult))
            {
                _tempKey = arg.Key;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Accepted;
                context.Result = cachedResult as JsonResult;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_tempKey == null)
            {
                return;
            }

            if (context.Result is JsonResult)
            {
                this.Cache.Set(this._tempKey, context.Result, 
                    new MemoryCacheEntryOptions()
                                {
                                    SlidingExpiration = TimeSpan.FromSeconds(this.SlidingExpiration)
                                }.SetAbsoluteExpiration(TimeSpan.FromSeconds(this.AbsoluteExpiration)));
            }
        }
    }
}
