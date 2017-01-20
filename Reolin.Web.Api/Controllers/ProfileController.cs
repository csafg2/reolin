using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.filters;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Api.ViewModels.profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
#pragma warning disable CS1591
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly IMemoryCache _cache;
        private readonly IFileService _fileService;

        public ProfileController(IProfileService service, IMemoryCache cache, IFileService fileService)
        {
            this._profileService = service;
            this._fileService = fileService;
            this._cache = cache;
        }
#pragma warning restore CS1591 

        private IProfileService ProfileService
        {
            get
            {
                return _profileService;
            }
        }


        /// <summary>
        /// Get all profiles that are associated with tag, result is cached for 60 * 60 seconds
        /// </summary>
        /// <param name="tag">the tag text to search for</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        [OutputCache(Key = "tag", AbsoluteExpiration = 60 * 60, SlidingExpiration = 5 * 60)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileByTagDTO>))]
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


        /// <summary>
        /// add a text description to the specified profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddDescription(ProfileAddDescriptionModel model)
        {
            Task addDescriptionTask = this.ProfileService.AddDescriptionAsync(model.Id, model.Description);
            Task addTagTask = this.ProfileService.AddTagAsync(model.Id, model.Description.ExtractHashtags());

            await Task.WhenAll(addDescriptionTask, addTagTask);

            return Ok();
        }

        /// <summary>
        /// adds an image the image collection of the profile
        /// </summary>
        /// <param name="model">a model that contians the profile id</param>
        /// <param name="files">image file</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [RequireValidModel]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> AddImage(AddImageToProfileViewModel model, IEnumerable<IFormFile> files)
        {
            // TODO: test it
            IFormFile file = files.FirstOrDefault() ?? Request.Form.Files[0];
            using (var stream = file.OpenReadStream())
            {
                string path = await this._fileService.SaveAsync(stream, file.FileName);
                int result = await this.ProfileService.AddProfileImageAsync(model.ProfileId, path);
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileId"></param>
        [Authorize]
        public async Task<IActionResult> Like(int profileId)
        {
            //TODO: test it
            int result = await this.ProfileService.AddLikeAsync(this.GetUserId(), profileId);

            return Ok(result);
        }
    }
}
