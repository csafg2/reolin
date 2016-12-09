using Microsoft.Extensions.Logging;
using System;

namespace Reolin.Web.Api.Infra.Logging
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private LogContext _context;
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

    public class SqlLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly LogContext _context;
        private readonly Func<string, LogLevel, bool> _filter;

        public SqlLogger(string categoryName, Func<string, LogLevel, bool> filter, LogContext context)
        {
            this._context = context;
            this._categoryName = categoryName;
            this._filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this._context;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter != null) && (_filter(_categoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel,
            EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                string message = $"{ logLevel }: {exception.Message}";
                this._context.Logs.Add(new Log() { Date = DateTime.Now, Message = message, Level = logLevel });
                this._context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }
    }
}