using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Domain;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Reolin.Web.Api.Controllers
{
    public class ProfileAddDescriptionModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Enter a valid Integer number")]
        public int Id { get; set; }
    }

    public class ProfileController : BaseController
    {
        private IPorofileService _profileService;

        public ProfileController(IPorofileService service)
        {
            this._profileService = service;
        }

        public IPorofileService ProfileService
        {
            get
            {
                return _profileService;
            }
        }


        public async Task<IActionResult> GetByTag(string tag)
        {
            throw new NotImplementedException();

            if(string.IsNullOrEmpty(tag))
            {
                ModelState.AddModelError("Error", "Tag is required");
                return BadRequest(this.ModelState);
            }

            var result = (await this.ProfileService.GetByTagAsync(tag))
                .Select(p => new
                {
                

                });
            return Json(new {  });
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
