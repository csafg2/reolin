using Microsoft.AspNetCore.Mvc;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.Threading.Tasks;
using System;

namespace Reolin.Web.Api.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class SuggestionController : BaseController
    {
        private ISuggestionService _service;

        public SuggestionController(ISuggestionService service)
        {
            this._service = service;
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
            return Ok(await this.Service.GetSuggestions(profileId));
        }
    }
}
