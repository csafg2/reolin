using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Reolin.Web.Api.Controllers
{

    /// <summary>
    /// Global Exception handler to prevent any unhandled exception to be serialized to client
    /// </summary>
    public class ErrorController : BaseController

    {
        private readonly IHostingEnvironment _environemnt;
        private readonly ILogger<ErrorController>   _logger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ErrorController(ILogger<ErrorController> logger, IHostingEnvironment env)
        {
            this._logger = logger;
            this._environemnt = env;
        }
#pragma warning restore CS1591

        /// <summary>
        /// this action gets called by runtime whenever an unhandled exception is thrown
        /// </summary>
        /// <returns></returns>
        public IActionResult SomeThingWentWrong()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (DebugEnabled(error))
            {
                return Error(error.Error.Message + error.Error.StackTrace);
            }

            return Error("Something went wrong, please try again later.");
        }

        private bool DebugEnabled(IExceptionHandlerFeature error)
        {
            return true;
            return error?.Error != null;
            return _environemnt.IsDevelopment() && error?.Error != null;
        }
    }
}
