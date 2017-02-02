using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Reolin.Web.Api.Infra.Filters
{
    /// <summary>
    /// caches the api response 
    /// </summary>
    public class OutputCacheAttribute : Attribute, IFilterFactory
    {
        /// <summary>
        /// the key to associate cached object with
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// amount of time to keep the object in memory if no request came to access it
        /// </summary>
        public int SlidingExpiration { get; set; }

        /// <summary>
        /// absolute amount of time to keep object in memory
        /// </summary>
        public int AbsoluteExpiration { get; set; }

        /// <summary>
        /// Determines if object can be used to serve multiple requests
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Instantiates a new CacheFilter which implements IFilterMetadata
        /// </summary>
        /// <param name="serviceProvider">a service provider object to resolve the underlying caching store from.</param>
        /// <returns></returns>
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
