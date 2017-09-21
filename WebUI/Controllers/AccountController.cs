using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController: Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RegisterWork()
        {
            return View();
        }

        public ActionResult RegisterCV()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
