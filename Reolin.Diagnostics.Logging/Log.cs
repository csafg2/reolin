using System;
using Microsoft.Extensions.Logging;

namespace Reolin.Web.Api.Infra.Logging
{

    public class Log
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public LogLevel Level { get; set; }
        public DateTime Date { get; set; }
    }
}