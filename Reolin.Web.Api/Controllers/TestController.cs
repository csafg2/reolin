using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;

namespace Reolin.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        [Route("[controller]/[action]")]
        public ActionResult Index()
        {
            return Json(new { text = "hellow" });
        } 
    }
}
