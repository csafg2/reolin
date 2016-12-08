using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;

namespace Reolin.Web.Api.Controllers
{
    public class ErrorController: BaseController
    {
        public IActionResult SomeThingWentWrong()
        {
            return Content("SOME THING WENT WRONG");
        }
    }
}
