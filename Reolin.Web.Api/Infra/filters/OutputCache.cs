using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Reolin.Web.Api.Infra.filters
{
    /// <summary>
    /// caches the api response 
    /// </summary>
    public class OutputCacheAttribute : Attribute, IFilterFactory
    {
        /// <summary>
        /// the key in request to associate cache with
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// amount of time to keep the object in memory if not request came to access it
        /// </summary>
        public int SlidingExpiration { get; set; }
        public int AbsoluteExpiration { get; set; }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            IMemoryCache cache = serviceProvider.GetService(typeof(IMemoryCache)) as IMemoryCache;
            return new CacheFilter(cache)
            {
                Key = this.Key,
                SlidingExpiration = this.SlidingExpiration,
                AbsoluteExpiration = this.AbsoluteExpiration
            };
        }
    }
}
