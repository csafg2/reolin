using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
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


        private async Task SignInInternal(string userName, string[] roles)
        {
            throw new NotImplementedException();
            //var authProperties = new AuthenticationProperties();
            //var identity = new ClaimsIdentity(Config.AUTHENTICATION_SCHEME);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userName));
            ////identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //foreach (var item in roles)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, item));
            //}
            //var principal = new ClaimsPrincipal(identity);
            //var claimsPrincipal = new ClaimsPrincipal(identity);
            //await HttpContext.Authentication.SignInAsync(Config.AUTHENTICATION_SCHEME,
            //                                                claimsPrincipal,
            //                                                authProperties);
        }
    }
}
