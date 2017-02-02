using Microsoft.Extensions.Logging;
using System;

namespace Reolin.Web.Api.Infra.Logging
{

    /// <summary>
    /// provides a new instance of Reolin.Web.Api.Infra.Logging.SqlLogger class
    /// </summary>
    public class SqlLoggerProvider : ILoggerProvider
    {
        private readonly LogContext _context;
        private readonly Func<string, LogLevel, bool> _filter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">a func that determines weather to log a message or not (according to level)</param>
        /// <param name="context"></param>
        public SqlLoggerProvider(Func<string, LogLevel, bool> filter, LogContext context)
        {
            _context = context;
            _filter = filter;
        }

#pragma warning disable CS1591
        public ILogger CreateLogger(string categoryName)
        {
            return new SqlLogger(categoryName, _filter, _context);
        }

        public void Dispose()
        {
        }
    }
}
