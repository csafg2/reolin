#pragma warning disable CS1591
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.Filters;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Api.ViewModels.profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    [RequireValidModel]
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

            return Ok(result);
        }

        /// <summary>
        /// add a text description to the specified profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [RequireValidModel]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddDescription(ProfileAddDescriptionModel model)
        {
            Task addDescriptionTask = this.ProfileService.AddDescriptionAsync(model.Id, model.Description);
            Task addTagTask = this.ProfileService.AddTagAsync(model.Id, model.Description.ExtractHashtags());

            await Task.WhenAll(addDescriptionTask, addTagTask);

            return Ok();
        }

        /// <summary>
        /// adds an image to image collection of the profile
        /// </summary>
        /// <param name="model">a model that contians the profile id</param>
        /// <param name="file">image file</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [RequireValidModel]
        [RequestFormSizeLimit(3000)]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> AddImage(AddImageToProfileViewModel model, IFormFile file)
        {
            if (file == null)
                return BadRequest();

            using (var stream = file.OpenReadStream())
            {
                // TODO: fix this to accept other info
                string path = await this._fileService.SaveAsync(stream, file.FileName);
                int result = await this.ProfileService.AddProfileImageAsync(model.ProfileId, model.CategoryId, model.Subject, model.Description, path);
                return Ok(path);
            }
        }

        /// <summary>
        /// Add a like entry to the specified Profile, note that the userId must be present in the request
        /// </summary>
        /// <param name="profileId">the Id of profile which has been liked</param>
        [HttpPost]
        [Authorize]
        [Route("/User/LikeProfile/{profileId}")]
        public async Task<IActionResult> Like(int profileId)
        {
            await this.ProfileService.AddLikeAsync(this.GetUserId(), profileId);

            return Ok();
        }

        /// <summary>
        /// Creates a new Work Profile for specified user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>the address in which the profile info is create an accessible to consume</returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> CreateWork(ProfileCreateModel model)
        {
            Profile result = await this.ProfileService.CreateWorkAsync(this.GetUserId(),
                new CreateProfileDTO()
                {
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Name = model.Name,
                    City = model.City,
                    Country = model.Country,
                    JobCategoryId = model.JobCategoryId
                });

            return Created($"/Profile/GetInfo/{result.Id}", (ProfileInfoDTO)result);
        }

        /// <summary>
        /// Creates a new Personal Profile for the specified user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>the address in which the profile info is create an accessible to consume</returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> CreatePersonal(ProfileCreateModel model)
        {
            Profile result = await this.ProfileService.CreatePersonalAsync(this.GetUserId(),
                new CreateProfileDTO()
                {
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    City = model.City,
                    Country = model.Country,
                    Name = model.Name
                });

            return Created($"/Profile/GetInfo/{result.Id}", (ProfileInfoDTO)result);
        }
        
        /// <summary>
        /// Retrieve Profile information by Id
        /// </summary>
        /// <param name="id">profieId</param>
        /// <returns></returns>
        [Authorize]
        [Route("/[Controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetInfo(int id)
        {
            var result = await this.ProfileService.QueryInfoAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get all profiles that are related to this profile by tag
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]/{profileId}")]
        public async Task<IActionResult> GetRelated(int profileId)
        {
            var data = (await this.ProfileService.GetRelatedProfiles(profileId))
                .Select(p => new RelatedProfileDTO()
                {
                    Description = p.Description,
                    Name = p.Name,
                    Id = p.Id
                });

            return Ok(data);
        }


        /// <summary>
        /// Retrieves all profiles withing the specified range by meter which are associated with the desired #tag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SearchInAreaByTag(SearchProfilesInRangeModel model)
        {
            var data = await this
                .ProfileService
                .GetInRangeAsync(model.Tag, model.SearchRadius, model.SourceLatitude, model.SourceLongitude);
            return Ok(data);
        }


        /// <summary>
        /// Updates specified fields
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileEditModel model)
        {
            await this.ProfileService.EditProfile(model.ProfileId, model.City, model.Country, model.Name);

            return Ok();
        }

        /// <summary>
        /// modify education info of the profile
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> EditEducation(EducationEditDTO dto)
        {
            int result = await this.ProfileService.EditEducation(dto.ProfileId, dto);
            return Ok(result);
        }


        /// <summary>
        /// Add a skill to the specified profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddSkill(ProfileAddSkillModel model)
        {
            await this.ProfileService.AddSkill(model.ProfileId, model.SkillName);
            
            return Ok();
        }


        /// <summary>
        /// add a new network to networks collection of the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [RequireValidModel]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddNetwork(ProfileAddNetworkModel model)
        {
            await this.ProfileService.AddSocialNetwork(model.ProfielId, model.SocialNetworkId, model.Url);

            return Ok();
        }
        
        /// <summary>
        /// Gets a list of all available job categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AvailableJobCategories()
        {
            return Ok(await this.ProfileService.QueryJobCategories());
        }
    }
}
