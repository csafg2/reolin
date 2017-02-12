using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Reolin.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Contact()
        {
          
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
