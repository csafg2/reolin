using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Reolin.Web.Controllers
{
    public class AccountController : Controller
    {
        [Route("/Login")]
        public Task<ActionResult> Login()
        {
            throw new NotImplementedException();
        }

        public ActionResult RegisterCV()
        {
            return View();
        }

        public ActionResult RegisterWork()
        {
            return View();
        }
    }
}
