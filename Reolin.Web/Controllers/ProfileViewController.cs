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
    }
}
