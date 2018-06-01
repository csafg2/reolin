#pragma warning disable CS1591
using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.filters;
using Reolin.Web.Api.Infra.Filters;
using Reolin.Web.Api.Infra.GeoServices;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.Api.Models;
using Reolin.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using static Reolin.Web.ViewModels.ProfileCreateModel;

namespace Reolin.Web.Api.Controllers
{
    [RequireValidModel]
    [EnableCors("AllowAll")]
    [InvalidOperationSerializerFilter]
    public class ProfileController : BaseController
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IProfileService _profileService;
        private readonly IMemoryCache _cache;
        private readonly IFileService _fileService;
        private readonly IGeoService _geoService = new FakeGeoService();
        private readonly IImageCategoryService _imageCategoryService;

        private DataContext _context;
        public ProfileController(DataContext context,
            IProfileService service, 
            IMemoryCache cache, 
            IFileService fileService, 
            IImageCategoryService imageCategoryService,
            IHttpContextAccessor httpContextAccessor)
        {
            this._httpContext = httpContextAccessor;
            this._profileService = service;
            this._fileService = fileService;
            this._cache = cache;
            this._imageCategoryService = imageCategoryService;
            this._context = context;
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
        /// Set Fax
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="fax"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> SetFax(int profileId, string fax)
        {
            var profile = await this._context
                .Profiles
                .FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.Fax = fax;
            await _context.SaveChangesAsync();

            return Ok();
        }



        /// <summary>
        /// Set address
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> SetPhoneNumber(int profileId, string phoneNumber)
        {
            var profile = await this._context
                .Profiles
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.PhoneNumber = phoneNumber;
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Set address
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> SetAddressNumber(int profileId, string address)
        {
            var profile = await this._context
                .Profiles
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null)
            {
                return NotFound();
            }

            if (profile.Address == null)
            {
                profile.Address = new Address();
            }

            profile.Address.AddressString = address;
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Get all profiles that are associated with tag, result is cached for 60 * 60 seconds
        /// </summary>
        /// <param name="tag">the tag text to search for</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        //[OutputCache(Key = "tag", AbsoluteExpiration = 60 * 60, SlidingExpiration = 5 * 60)]
        public async Task<IActionResult> GetByTag(string tag)
        {
            List<ProfileSearchResult> result = null;

            if (string.IsNullOrEmpty(tag))
            {
                result = await this._context.Profiles.OrderByDescending(p => p.Id)
                    .Select(p => new ProfileSearchResult()
                    {
                        Id = p.Id,
                        City = p.Address.City,
                        Country = p.Address.Country,
                        Description = p.Description,
                        Latitude = p.Address.Location.Latitude,
                        Longitude = p.Address.Location.Longitude,
                        Name = p.Name,
                        Icon = p.IconUrl,
                        IsWork = p.Type == ProfileType.Work
                    })
                    .Take(20)
                    .ToListAsync();
            }
            else
            {
                result = await this.ProfileService.GetByTagAsync(tag)
                .OrderByDescending(p => p.Id)
                .Take(20)
                .ToListAsync();
            }

            return Ok(result);
        }

        /// <summary>
        /// add a text description to the specified profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/User/LikeProfile/{profileId}")]
        public async Task<IActionResult> Like(int profileId)
        {
            var senderId = this.GetUserId();
            var likeCount = await this._context
               .Likes
               .Where(l => l.TargetProfileId == profileId)
               .CountAsync();

            if (await _context.Likes.AnyAsync(l => l.SenderId == senderId && l.TargetProfileId == profileId))
            {
                await _context
                    .Likes
                    .Where(l => l.SenderId == senderId && l.TargetProfileId == profileId)
                    .DeleteAsync();
                return Json(new { Count = --likeCount });
            }
            else
            {
                await this.ProfileService.AddLikeAsync(this.GetUserId(), profileId);
            }

            return Json(new { Count = ++likeCount });
        }


        /// <summary>
        /// Creates a new Work Profile for specified user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>the address in which the profile info is create an accessible to consume</returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                    SubJobCategoryId = model.SubJobCategoryId,

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            await this.ProfileService.EditProfile(model.ProfileId, model.Name);

            return Ok();
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
        /// get the list of skills
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSkills(int profileId)
        {
            var skills = await _context.Skills.Where(s => s.Profiles.Any(p => p.Id == profileId)).ToListAsync();

            return Ok(skills);
        }

        /// <summary>
        /// add a new network to networks collection of the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddNetwork(ProfileAddNetworkModel model)
        {
            await this.ProfileService.AddSocialNetwork(model.ProfileId,
                model.SocialNetworkId,
                model.Url,
                model.Description);

            return Ok();
        }



        /// <summary>
        /// Edit netowrk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> EditNetwork(ProfileAddNetworkModel model)
        {
            await this._context
                .ProfileNetworks
                .Where(p => p.ProfileId == model.ProfileId && p.NetworkId == model.SocialNetworkId)
                .UpdateAsync(s => new ProfileNetwork()
                {
                    Url = model.Url,
                    Description = model.Description
                });

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
        /// search for morteza pear
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> OptionalFind(OptionalSearch query)
        {
            DbGeography sourceLocation = GeoHelpers.FromLongitudeLatitude(query.SoruceLong, query.SourceLat);
            this.TryGetUserId(out int userId);
            var result = await this._context
                .Profiles
                .Where(p =>
                    p.JobCategories.Any(jc => jc.Id == query.JobCategoryId)
                        ||
                    p.JobCategories.Any(j => j.Id == query.SubJobCategoryId))
                .Where(p => p.Name.Contains(query.SearchTerm) || p.Tags.Any(t => t.Name.Contains(query.SearchTerm)))
                .Where(p => p.Address.Location.Distance(sourceLocation) < query.Distance)
                .Select(p => new ProfileSearchResult()
                {
                    Id = p.Id,
                    City = p.Address.City,
                    Country = p.Address.Country,
                    Description = p.Description,
                    Latitude = p.Address.Location.Latitude,
                    Longitude = p.Address.Location.Longitude,
                    Name = p.Name,
                    DistanceWithSource = p.Address.Location.Distance(sourceLocation),
                    IsLiked = p.ReceivedLikes.Any(l => l.SenderId == userId)
                })
                .OrderByDescending(p => p.Id)
                .Take(50)
                .ToListAsync();

            return Ok(result);
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Find(ProfileSearchModel model)
        {
            List<ProfileSearchResult> result = null;
            
            this.TryGetUserId(out int userId);
            if (model.JobCategoryId != null)
            {
                result = await this.ProfileService.SearchByCategoriesTagsAndDistance(
                            (int)model.JobCategoryId,
                            model.SubJobCategoryId,
                            model.SearchTerm,
                            model.SourceLatitude,
                            model.SourceLongitude,
                            userId,
                            model.Distance);
            }
            else
            {
                result = await this.ProfileService.SearchBySubCategoryTagsAndDistance(
                            model.SubJobCategoryId,
                            model.SearchTerm,
                            model.SourceLatitude,
                            model.SourceLongitude,
                            userId,
                            model.Distance);
            }

            return Ok(result);
        }

        /// <summary>
        /// find by name and distance
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ByName(SearchByNameQuery model)
        {
            var source = GeoHelpers.FromLongitudeLatitude(model.Longitude, model.Lat);
            Expression<Func<Profile, bool>> filter = p =>
                (p.Address.Location.Distance(source) <= model.Radius) && p.Name.Contains(model.Name);
            
            if (string.IsNullOrEmpty(model.Name))
            {
                filter = p => true;
            }
            
            this.TryGetUserId(out int userId);
            var query = this._context
                            .Profiles
                            .Where(filter)
                                .Select(p => new ProfileRedisCacheDTO()
                                {
                                    Id = p.Id,
                                    Description = p.Description,
                                    Latitude = p.Address.Location.Latitude,
                                    Longitude = p.Address.Location.Longitude,
                                    Name = p.Name,
                                    City = p.Address.City,
                                    Country = p.Address.Country,
                                    LikeCount = p.ReceivedLikes.Count(),
                                    DistanceWithSource = p.Address.Location.Distance(source),
                                    IsLiked = p.ReceivedLikes.Any(l => l.SenderId == userId)
                                });
            
            var result = await query
                                .Take(20)
                                .ToListAsync();

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
        /// Get the address of profile
        /// </summary>
        /// <param name="profielId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetAddres(int profielId)
        {
            var profile = await this._context
                .Profiles
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == profielId);

            return Ok(new { Address = profile?.Address?.AddressString });
        }

        /// <summary>
        /// retrieve latest comments for prfile page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [RequireValidModel]
        public async Task<IActionResult> AddTag(AddTagModel model)
        {
            await this.ProfileService.AddTagAsync(model.ProfileId, new[] { model.Tag });

            return Ok();
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RemoveTag(int profileId, int tagId)
        {
            var profile = await this._context.Profiles.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == profileId);
            var tag = profile.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag != null)
            {
                profile.Tags.Remove(tag);
                await this._context.SaveChangesAsync();
            }
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
            return Ok(await ProfileService.GetTags(id));
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Location(int id)
        {
            var location = (await this.ProfileService.GetLocation(id)).Location;
            return Ok(new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
        }

        /// <summary>
        /// ست کردن آیکون پروفایل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <summary>
        /// Get all certificates for profile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetIcon(SetProfileIconModel model)
        {
            var profile = await _context
                .Profiles
                .FirstOrDefaultAsync(c => c.Id == model.ProfileId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.IconUrl = model.IconUrl;
            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetCityAndState(SetCityStateModel model)
        {
            var profile = await _context
                .Profiles
                .Include(p => p.Address)
                .FirstOrDefaultAsync(c => c.Id == model.ProfileId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.Address.City = model.City;
            profile.Address.Country = model.Country;

            await _context.SaveChangesAsync();
            return Ok(profile);
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetLatLong(SetLatLongMode model)
        {
            var profile = await _context
                .Profiles
                .Include(p => p.Address)
                .FirstOrDefaultAsync(c => c.Id == model.ProfileId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.Address.Location = GeoHelpers.FromLongitudeLatitude(model.Long, model.Lat);

            await _context.SaveChangesAsync();
            return Ok(profile);
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetFirstNameLastName(SetFirstNameLastName model)
        {
            var profile = await _context
                .Users
                .FirstOrDefaultAsync(c => c.Id == model.UserId);

            if (profile == null)
            {
                return NotFound();
            }


            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;

            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetMobile(SetMobile model)
        {
            var result = await this._context.Profiles
                .Where(p => p.Id == model.ProfileId)
                .UpdateAsync(p => new Profile() { Mobile = model.Mobile });

            return Ok();
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SetPostal(SetPostal model)
        {
            var result = await this._context.Profiles
                .Where(p => p.Id == model.ProfileId)
                .UpdateAsync(p => new Profile() { Postal = model.Postal });

            return Ok();
        }
    }

    public class SetProfilePropertyModel
    {
        public int ProfileId { get; set; }

    }

    public class SetFax : SetProfilePropertyModel
    {
        public string Fax { get; set; }
    }

    public class SetMobile : SetProfilePropertyModel
    {
        public string Mobile { get; set; }
    }

    public class SetPostal : SetProfilePropertyModel
    {
        public string Postal { get; set; }
    }


    public class SetFirstNameLastName
    {
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string FirstName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string LastName { get; set; }
    }

    public class SetLatLongMode
    {
        public int ProfileId { get; set; }
        public double Lat { get; set; }
        public double Long
        {
            get; set;
        }

    }

    public class SetCityStateModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid ProfileID")]
        public int ProfileId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "invalid city")]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "invalid State")]
        public string Country { get; set; }
    }
}