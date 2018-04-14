using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Reolin.Data;
using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Reolin.Data.Domain;
using System.Data.Entity;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class ImageController : Controller
    {
        private DataContext _context;
        public ImageController(DataContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// get image by tag
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetByTag(int profileId, int tagId)
        {
            var images = await _context.Images
                .Where(i => i.ProfileId == profileId && i.Tags.Any(t => t.Id == tagId))
                .Select(i => new
                {
                    i.Id,
                    i.Path,
                    i.ProfileId
                })
                .ToListAsync();

            return Ok(images);
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteImage(int imageId)
        {
            await _context.Images.Where(i => i.Id == imageId).DeleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteImageCategory(int id)
        {
            await _context.ImageCategories.Where(i => i.Id == id).DeleteAsync();
            return Ok();
        }
    }
}