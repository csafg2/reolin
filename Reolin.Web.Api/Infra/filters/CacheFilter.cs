using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Reolin.Web.Api.Infra.filters
{
    /// <summary>
    /// used to store an object into the cach
    /// </summary>
    public class CacheFilter : ActionFilterAttribute
    {
        private readonly IMemoryCache _cache;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public CacheFilter(IMemoryCache cache)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            this._cache = cache;
        }

        /// <summary>
        /// the key to attache the cached object with
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// the absolute time before object is disposed from undelerlying cache store
        /// </summary>
        public int AbsoluteExpiration { get; set; }
        
        /// <summary>
        /// the times in second to keep object in cache if no request came for it.
        /// </summary>
        public int SlidingExpiration { get; set; }

        private IMemoryCache Cache
        {
            get
            {
                return _cache;
            }
        }

        private object _tempKey;

        /// <summary>
        /// excecuted before the action method is called by runtime
        /// </summary>
        /// <param name="context"></param>
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
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = cachedResult as JsonResult;
            }
        }

        /// <summary>
        /// excecuted after the action method is called by runtime
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_tempKey == null)
            {
                return;
            }

            if (!(context.Result is JsonResult))
            {
                throw new NotSupportedException("Only 'JsonResult' is supported for caching");
            }

            this.Cache.Set(this._tempKey, context.Result, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(this.SlidingExpiration)
            }.SetAbsoluteExpiration(TimeSpan.FromSeconds(this.AbsoluteExpiration)));
        }
    }
}
