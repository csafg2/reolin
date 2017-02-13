using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
