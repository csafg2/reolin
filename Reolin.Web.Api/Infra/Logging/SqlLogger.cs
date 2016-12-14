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
            if (filter == null)
            {
                throw new ArgumentException(nameof(filter));
            }

            this._context = context;
            this._categoryName = categoryName;
            this._filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter(_categoryName, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel) || exception == null)
            {
                return;
            }

            try
            {
                string message = $"{ logLevel }: {exception.Message} : { exception.StackTrace}";
                this._context.Logs.Add(new Log() { Date = DateTime.Now, Message = message, Level = logLevel });
                this._context.SaveChangesAsync().Forget();
            }
            catch (Exception)
            {
                // we do not accept any exception here so, just ignore it.
            }
        }
    }
}