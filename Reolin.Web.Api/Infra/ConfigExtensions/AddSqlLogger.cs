using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.Logging;
using System;

namespace Reolin.Web.Api.Infra.ConfigExtensions
{
    public static class AddSqlLoggerExtension
    {
        public static void AddSqlLogger(this ILoggerFactory source, string connectionString)
        {
            Func<string, LogLevel, bool> filter = (cat, level) => level == LogLevel.Error || level == LogLevel.Critical;
            var context = new LogContext(connectionString);
            source.AddProvider(new SqlLoggerProvider(filter, context));
        }
    }
}
