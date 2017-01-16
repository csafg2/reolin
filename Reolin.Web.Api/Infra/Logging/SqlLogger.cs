using Microsoft.Extensions.Logging;
using System;

namespace Reolin.Web.Api.Infra.Logging
{
    /// <summary>
    /// loggs messages into sql server
    /// </summary>
    public class SqlLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly LogContext _context;
        private readonly Func<string, LogLevel, bool> _filter;

#pragma warning disable CS1591
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

#pragma warning restore CS1591

        /// <summary>
        /// determine logging is Enabled by this class for specified log level
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter(_categoryName, logLevel);
        }

        /// <summary>
        /// logs the exception into underlying log provider
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">the logLevel</param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception">the exception to be serialized</param>
        /// <param name="formatter">string message formatter to formatt log message</param>
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel) || exception == null)
            {
                return;
            }

            try
            {
                string message = $"{ logLevel }: {exception.Message} : { exception.StackTrace}";
                this._context.Logs.Add(new Log() { Date = DateTime.Now, Message = message, Level = logLevel });
                await this._context.SaveChangesAsync();//.Forget();
            }
            catch (Exception)
            {
                // we do not accept any exception here so, just ignore it.
            }
        }
    }
}