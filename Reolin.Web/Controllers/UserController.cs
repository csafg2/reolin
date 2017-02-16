using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reolin.Web.Controllers
{
    public class UserController: BaseController
    {
        public ActionResult ChooseYourProfile()
        {
            return View();
        }

        public ActionResult SetActive(int id)
        {
            HttpContext.Session.SetInt32("CurrentProfileId", id);
            return Redirect("/");
        }
    }

}
