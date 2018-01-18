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