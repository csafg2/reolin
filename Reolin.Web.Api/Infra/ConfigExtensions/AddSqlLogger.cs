using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.Logging;
using System;

namespace Reolin.Web.Api.Infra.ConfigExtensions
{
    internal static class AddSqlLoggerExtension
    {
        public static void UseSqlLogger(this ILoggerFactory source, string connectionString)
        {
            Func<string, LogLevel, bool> filter = 
                (cat, level) => level == LogLevel.Error || level == LogLevel.Critical;
            LogContext context = new LogContext(connectionString);
            source.AddProvider(new SqlLoggerProvider(filter, context));
        }
    }
}
