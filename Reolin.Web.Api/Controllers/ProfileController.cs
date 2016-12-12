﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data.Entity;
using Reolin.Web.Api.ViewModels.profile;

namespace Reolin.Web.Api.Controllers
{

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
            if (string.IsNullOrEmpty(tag))
            {
                ModelState.AddModelError("Error", "Tag is required");
                return BadRequest(this.ModelState);
            }

            var result = await this.ProfileService.GetByTagAsync(tag)
                .Select(p => new
                {
                    Description = p.Description,
                    Address = p.Address
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
