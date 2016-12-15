using Microsoft.Extensions.Logging;
using System;

namespace Reolin.Web.Api.Infra.Logging
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private readonly LogContext _context;
        private readonly Func<string, LogLevel, bool> _filter;

        public SqlLoggerProvider(Func<string, LogLevel, bool> filter, LogContext context)
        {
            _context = context;
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SqlLogger(categoryName, _filter, _context);
        }

        public void Dispose()
        {
        }
    }
}
