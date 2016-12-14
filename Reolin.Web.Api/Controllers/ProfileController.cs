using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data.Entity;
using Reolin.Web.Api.ViewModels.profile;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Reolin.Web.Api.Infra.filters;

namespace Reolin.Web.Api.Controllers
{

    public class ProfileController : BaseController
    {
        private readonly IPorofileService _profileService;
        private readonly IMemoryCache _cache;

        public ProfileController(IPorofileService service, IMemoryCache cache)
        {
            this._profileService = service;
            this._cache = cache;
        }

        public IPorofileService ProfileService
        {
            get
            {
                return _profileService;
            }
        }

        [OutputCache(Key = "tag", AbsoluteExpiration =  60 * 60, SlidingExpiration = 5 * 60)]
        public async Task<IActionResult> GetByTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                ModelState.AddModelError("Error", "Tag is required");
                return BadRequest(this.ModelState);
            }
            
            var result = await this.ProfileService.GetByTagAsync(tag)
                        .Select(p => new
                        {
                            Description = p.Description,
                            Address = p.Address,
                            p.Address.Location
                        }).ToListAsync();
            
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDescription(ProfileAddDescriptionModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            Task addDescriptionTask = this.ProfileService.AddDescriptionAsync(model.Id, model.Description);
            Task addTagTask = this.ProfileService.AddTagAsync(model.Id, model.Description.ExtractHashtags());

            await Task.WhenAll(addDescriptionTask, addTagTask);

            return Ok();
        }
    }
}
