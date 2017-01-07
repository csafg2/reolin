using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.filters;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels.profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IPorofileService _profileService;
        private readonly IMemoryCache _cache;
        private readonly IFileService _fileService;

        public ProfileController(IPorofileService service, IMemoryCache cache, IFileService fileService)
        {
            this._profileService = service;
            this._fileService = fileService;
            this._cache = cache;
        }

        public IPorofileService ProfileService
        {
            get
            {
                return _profileService;
            }
        }

        [OutputCache(Key = "tag", AbsoluteExpiration = 60 * 60, SlidingExpiration = 5 * 60)]
        public async Task<IActionResult> GetByTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                ModelState.AddModelError("Error", "Tag is required");
                return BadRequest(this.ModelState);
            }

            var result = await this.ProfileService.GetByTagAsync(tag).ToListAsync();

            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDescription(ProfileAddDescriptionModel model)
        {
            Task addDescriptionTask = this.ProfileService.AddDescriptionAsync(model.Id, model.Description);
            Task addTagTask = this.ProfileService.AddTagAsync(model.Id, model.Description.ExtractHashtags());

            await Task.WhenAll(addDescriptionTask, addTagTask);

            return Ok();
        }


        [HttpPost]
        [Authorize]
        [RequireValidModel]
        public async Task<ActionResult> AddImage(AddImageToProfileViewModel model, IEnumerable<IFormFile> files)
        {
            IFormFile file = files.FirstOrDefault() ?? Request.Form.Files[0];
            using (var stream = file.OpenReadStream())
            {
                string path = await this._fileService.SaveAsync(stream, file.FileName);
                int result = await this.ProfileService.AddProfileImageAsync(model.ProfileId, path);
            }

            return Ok();
        }
    }

}
