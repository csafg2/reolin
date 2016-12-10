using Microsoft.Extensions.Logging;
using System;

namespace Reolin.Web.Api.Infra.Logging
{
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
            return null;
            //return this._context;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter(_categoryName, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                if(exception == null)
                {
                    return;
                }

                string message = $"{ logLevel }: {exception.Message}";
                this._context.Logs.Add(new Log() { Date = DateTime.Now, Message = message, Level = logLevel });
                this._context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // IGNORE this fucking exception :D
            }
        }
    }
}