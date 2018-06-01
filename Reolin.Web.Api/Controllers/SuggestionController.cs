using Microsoft.AspNetCore.Mvc;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using Reolin.Web.Api.Models;
using Reolin.Data;
using System.Linq;
using System.Data.Entity;
using System.Dynamic;
using Reolin.Data.Domain;
using Newtonsoft.Json;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Reolin.Web.Api.Controllers
{
#pragma warning disable CS1591
    [EnableCors("AllowAll")]
    public class SuggestionController : BaseController
    {
        private readonly ISuggestionService _service;
        private readonly DataContext _dataContext;

        public SuggestionController(ISuggestionService service, DataContext dataContext)
        {
            this._service = service;
            this._dataContext = dataContext;
        }

        private ISuggestionService Service
        {
            get
            {
                return _service;
            }
        }

#pragma warning restore CS1591

        /// <summary>
        /// get top 20
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Top()
        {
            var result = await this._dataContext
                .Suggestions
                .OrderByDescending(s => s.DateCreated)
                .Select(s => new
                {
                    s.Id,
                    s.Description,
                    s.DateCreated,
                    s.Title,
                    s.ProfileId,
                    s.Image,
                    Icon = s.Profile.IconUrl,
                    City = s.Profile.Address.City,
                    Country = s.Profile.Address.Country
                })
                .Take(10)
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// add a suggestion for this profile, tags should will be separated by # sign
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Create(SuggestionCreateModel model)
        {
            var suggestion = await this.Service.AddSuggestion(model);
            foreach (var item in model.Description.ExtractHashtags())
            {
                await Service.AddTag(suggestion.Id, item);
            }

            return Ok();
        }


        /// <summary>
        /// get all suggestions for the specified profile
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSuggestions(int profileId)
        {
            var result = await this._dataContext
                          .Suggestions
                          .Where(s => s.ProfileId == profileId)
                          .Select(s => new
                          {
                              s.Id,
                              s.Description,
                              s.Image,
                              s.ProfileId,
                              Profile = new
                              {
                                  Id = s.ProfileId,
                                  IsWork = s.Profile.Type == ProfileType.Work
                              },
                              s.Title,
                              Tags = s.Tags.Select(t => new { t.Id, t.Name })
                          })
                          .ToListAsync();

            return Json(result);
        }

        /// <summary>
        /// search categorories
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Search(SearchSuggestionModel model)
        {
            var q = this._dataContext.Suggestions.AsQueryable();
            if (model.SubCategoryId != null)
            {
                q = q.Where(s => s.Profile.JobCategories
                         .Any(c =>
                              c.IsSubCategory == true
                                && 
                              c.Id == model.SubCategoryId));
            }

            if (!string.IsNullOrEmpty(model.Query))
            {
                q = q.Where(s => s.Description.Contains(model.Query) || s.Profile.Tags.Any(t => t.Name.Contains(model.Query)));
            }

            var sourceLocation = GeoHelpers.FromLongitudeLatitude(model.SountLong, model.SourceLat);

            var result = await q
                .Where(s => s.Profile.Address.Location.Distance(sourceLocation) < 100_000)
                .Select(s => new
                {
                    s.Id,
                    s.Description,
                    s.DateCreated,
                    s.Title,
                    s.ProfileId,
                    s.Profile.IconUrl,
                    s.Profile.Address.City,
                    s.Profile.Address.Country,
                    Location = new
                    {
                        s.Profile.Address.Location.Latitude,
                        s.Profile.Address.Location.Longitude
                    }
                })
                //.OrderByDescending(s => s.Distance)
                .Take(20)
                .ToListAsync();

            return Ok(result);
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await this._dataContext
                .Suggestions
                .Where(s => s.Id == id)
                .DeleteAsync();

            return Ok(result);
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Edit(SuggestionEditModel model)
        {
            var result = await this._dataContext
                .Suggestions
                .Where(s => s.Id == model.Id)
                .UpdateAsync(s => new Suggestion()
                {
                    Title = model.Title,
                    Description = model.Description
                });

            return Ok(result);
        }
    }

    public class SuggestionEditModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid Text")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
