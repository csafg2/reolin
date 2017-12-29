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
                .Take(20)
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
                              Id = s.Id,
                              Image = s.Image,
                              ProfileId = s.ProfileId,
                              Profile = new
                              {
                                  Id = s.ProfileId,
                                  IsWork = s.Profile.Type == ProfileType.Work
                              },
                              Title = s.Title,
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
                         .Any(c => c.IsSubCategory == true && c.Id == model.SubCategoryId));
            }

            if(string.IsNullOrEmpty(model.Query))
            {
                q = q.Where(s => s.Description.Contains(model.Query) || s.Profile.Tags.Any(t => t.Name.Contains(model.Query)));
            }

            var result = await q
                .OrderByDescending(s => s.DateCreated)
                .Select(s => new
                {
                    s.Id,
                    s.Description,
                    s.DateCreated,
                    s.Title,
                    s.ProfileId,
                    s.Image,
                    City = s.Profile.Address.City,
                    Country = s.Profile.Address.Country
                })
                .Take(20)
                .ToListAsync();

            return Ok(result);
        }
    }
}
