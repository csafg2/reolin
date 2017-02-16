using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Reolin.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
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
