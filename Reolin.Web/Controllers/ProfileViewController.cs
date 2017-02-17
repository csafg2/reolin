using Microsoft.AspNetCore.Mvc;

namespace Reolin.Web.Controllers
{
    public class ProfileViewController : Controller
    {
        [Route("/View/{id}")]
        public ActionResult ViewProfile(int id)
        {
            ViewBag.TargetProfileId = id;
            return View();
        }

        [Route("/Edit/{id}")]
        public ActionResult EditProfile(int id)
        {
            ViewBag.TargetProfileId = id;
            return View();
        }


    }
}
