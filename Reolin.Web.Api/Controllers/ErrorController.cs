using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Reolin.Web.Api.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly IHostingEnvironment _environemnt;
        private readonly ILogger<ErrorController>   _logger;

        public ErrorController(ILogger<ErrorController> logger, IHostingEnvironment env)
        {
            this._logger = logger;
            this._environemnt = env;
        }

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
            return error?.Error != null;
            return _environemnt.IsDevelopment() && error?.Error != null;
        }
    }
}
