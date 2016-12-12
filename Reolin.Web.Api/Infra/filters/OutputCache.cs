using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Reolin.Web.Api.Infra.filters
{
    public class OutputCacheAttribute : Attribute, IFilterFactory
    {
        public string Key { get; set; }
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
