using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Reolin.Web.Api.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly IHostingEnvironment _environemnt;
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger, IHostingEnvironment env)
        {
            this._logger = logger;
            this._environemnt = env;
        }
        
        public IActionResult SomeThingWentWrong()
        {
            IExceptionHandlerFeature error = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (error != null && _environemnt.IsDevelopment())
            {
                // TODO: LOG the error
                return Error(error.Error.Message);
            }

            return Error("Some thing went wrong, please try again later.");
        }
    }
}
