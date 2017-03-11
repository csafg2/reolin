#pragma warning disable CS1591
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.filters;
using Reolin.Web.Api.Infra.Filters;
using Reolin.Web.Api.Infra.GeoServices;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using static Reolin.Web.ViewModels.ProfileCreateModel;

namespace Reolin.Web.Api.Controllers
{
    [InvalidOperationSerializerFilter]
    [EnableCors("AllowAll")]
    [RequireValidModel]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly IMemoryCache _cache;
        private readonly IFileService _fileService;
        private readonly IGeoService _geoService = new FakeGeoService();
        private readonly IImageCategoryService _imageCategoryService;

        public ProfileController(IProfileService service, IMemoryCache cache, IFileService fileService, IImageCategoryService imageCategoryService)
        {
            this._profileService = service;
            this._fileService = fileService;
            this._cache = cache;
            this._imageCategoryService = imageCategoryService;
        }
#pragma warning restore CS1591 

        private IImageCategoryService ImageCategoryService
        {
            get
            {
                return _imageCategoryService;
            }
        }
        
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
        //[OutputCache(Key = "tag", AbsoluteExpiration = 60 * 60, SlidingExpiration = 5 * 60)]
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
        //[Authorize]
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
                int result = await ProfileService
                                    .AddProfileImageAsync(model.ProfileId,
                                        model.CategoryId,
                                        model.Subject,
                                        model.Description,
                                        path,
                                        model.TagIds);
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
                    JobCategoryId = model.JobCategoryId,
                    SubJobCategoryId = model.SubJobCategoryId
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
        [Authorize]
        public async Task<IActionResult> CreatePersonal(ProfileCreateModel model)
        {
            Profile result = await this.ProfileService.CreatePersonalAsync(this.GetUserId(),
                new CreateProfileDTO()
                {
                    Name = model.Name,
                    Description = model.Description,
                    City = model.City,
                    Country = model.Country,
                    PhoneNumber = model.PhoneNumber,
                    Latitude = model.Latitude,// ?? _geoService.GetGeoInfo(model.City, model.Country).Latitude,
                    Longitude = model.Longitude,// ?? _geoService.GetGeoInfo(model.City, model.Country).Longitude,
                    JobCategoryId = model.JobCategoryId,
                    SubJobCategoryId = model.SubJobCategoryId
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
            await this.ProfileService.AddSocialNetwork(model.ProfielId, model.SocialNetworkId, model.Description, model.Url);

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


        /// <summary>
        /// Finds matched profiels first filters by job categories (AND operation) 
        /// then by profile name
        /// then profiles that contain search term as tag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Find(ProfileSearchModel model)
        {
            List<ProfileSearchResult> result = null;
            if (model.JobCategoryId != null)
            {
                result = await this.ProfileService.SearchByCategoriesTagsAndDistance(
                            (int)model.JobCategoryId,
                            model.SubJobCategoryId,
                            model.SearchTerm,
                            model.SourceLatitude,
                            model.SourceLongitude,
                            model.Distance);
            }
            else
            {
                result = await this.ProfileService.SearchBySubCategoryTagsAndDistance(
                            model.SubJobCategoryId,
                            model.SearchTerm,
                            model.SourceLatitude,
                            model.SourceLongitude,
                            model.Distance);
            }

            return Ok(result);
        }


        /// <summary>
        /// returns basic information about the profile by it`s id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> BasicInfo(int id)
        {
            var data = await this.ProfileService.GetBasicInfo(id);
            return Ok(data);
        }


        /// <summary>
        /// retrieve latest comments for prfile page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        [Authorize]
        public async Task<IActionResult> LatestComments(int id)
        {
            var data = await this.ProfileService.GetLatestComments(id);
            return Ok(data);
        }

        /// <summary>
        /// adds desired to tags collection of the profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize]
        [RequireValidModel]
        public async Task<IActionResult> AddTag(AddTagModel model)
        {
            await this.ProfileService.AddTagAsync(model.ProfileId, new[] { model.Tag });
            return Ok();
        }


        /// <summary>
        /// return profile tags
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> GetTags(int id)
        {
            return Ok(await this.ProfileService.GetTags(id));
        }

        /// <summary>
        /// Get phone number of the profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> PhoneNumbers(int id)
        {
            return Ok(new
            {
                number = await this.ProfileService.GetPhoneNumbers(id)
            });
        }


        /// <summary>
        /// Get all related types
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> RelatedTypes(int id)
        {
            var data = await this.ProfileService.GetRelatedTypes(id);
            return Ok(data);
        }


        /// <summary>
        /// send a relation request to the target profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddRelation(RelateCreateModel model)
        {
            await this.ProfileService.AddRelate(model.SourceId, model.TargetId, model.Date, model.Description, model.RelatedTypeId);

            return Ok();
        }

        /// <summary>
        /// get all profile images
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Images(int id)
        {
            return Ok(await this.ProfileService.GetImages(id));
        }


        /// <summary>
        /// add a new imageCategory to ImageCategory collection of the profile
        /// </summary>
        /// <param name="model">the model to parsed as param</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddImageCategory(AddImageCategoryModel model)
        {
            await this.ProfileService.AddImageCategory(model.ProfileId, model.Name);
            return Ok();
        }


        /// <summary>
        /// get all Image categories
        /// </summary>
        /// <param name="id">the id of the profile to get categories</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> GetImageCategories(int id)
        {
            return Ok(await this.ImageCategoryService.GetByProfile(id));
        }

        /// <summary>
        /// Get all profiles that are related to this profile by request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> RequestRelatedProfiles(int id)
        {
            var data = await this.ProfileService.GetRequestRelatedProfiles(id);
            return Ok(data);
        }


        /// <summary>
        /// Add a certificate to profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddCertificate(CertificateCreateModel model)
        {
            await this.ProfileService.AddCertificateAsync(model.ProfileId, model.Year, model.Description);

            return Ok();
        }


        /// <summary>
        /// Get all certificates for profile
        /// </summary>
        /// <param name="id">the id of profile</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Certificates(int id)
        {
            var data = await this.ProfileService.GetCertificates(id);
            return Ok(data);
        }

        /// <summary>
        /// Add a related type to profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddRelatedType(AddRelatedTypeModel model)
        {
            await this.ProfileService.AddRelatedType(model.ProfileId, model.Type);
            return Ok();
        }


        /// <summary>
        /// Deletes a relation request
        /// </summary>
        /// <param name="id">the id of the relation to be deleted</param>
        /// <returns></returns>
        [Route("/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> DeleteRelationRequest(int id)
        {
            await this.ProfileService.DeleteRelationRequest(id);

            return Ok();
        }



        /// <summary>
        /// Confirm a request
        /// </summary>
        /// <param name="id">the id of the relation to be deleted</param>
        /// <returns></returns>
        [Route("/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> ConfirmRelationRequest(int id)
        {
            await this.ProfileService.ConfirmRelationRequest(id);

            return Ok();
        }

        /// <summary>
        /// get the location of the profile
        /// </summary>
        /// <param name="id">the id of profile</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Location(int id)
        {
            var location = (await this.ProfileService.GetLocation(id)).Location;
            return Ok(new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
        }
    }
}
