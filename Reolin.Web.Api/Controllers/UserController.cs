using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Reolin.Web.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMemoryCache _cache;
        private readonly IUserService _service;

        public UserController(IUserService service, IMemoryCache cache)
        {
            this._service = service;
            this._cache = cache;
        }

        public IUserService UserService
        {
            get
            {
                return _service;
            }
        }

        private IMemoryCache Cache
        {
            get
            {
                return _cache;
            }
        }


        // TODO: complete this method
        public async Task<IActionResult> SetFirstNameLastName(SetFirstNameLastNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            await this.UserService.SetUserInfo(this.UserId, model.FirstName, model.LastName);
            return Ok();
        }
    }
}
