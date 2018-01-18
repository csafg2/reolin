using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.Filters;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Data.Domain
{
    [EnableCors("AllowAll")]
    public class ExpController : BaseController
    {
        private readonly DataContext _context;

        public ExpController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this._context.Experiences.Where(e => e.Id == id).DeleteAsync();
            return Json(data);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Edit(int id, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("fuck you");
            }

            var data = await this._context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            data.Value = value;
            await this._context.SaveChangesAsync();
            return Json(data);
        }

        /// <summary>
        /// get exp
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> GetExp(int profileId)
        {
            var data = await this._context.Experiences.Where(e => e.ProfileId == profileId)
                .ToListAsync();

            return Json(data);
        }

        /// <summary>
        /// set about me
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SetAboutMe(AddAboutModel model)
        {
            var profile = await this._context.Profiles.FirstOrDefaultAsync(p => p.Id == model.ProfileId);
            if (profile == null)
            {
                return NotFound();
            }

            profile.AboutMe = model.Text;

            await _context.SaveChangesAsync();

            return Ok(profile.Experiences);
        }

        /// <summary>
        /// add experience
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireValidModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddExp(AddExpModel model)
        {
            var profile = await this._context.Profiles.Include(p => p.Experiences).FirstOrDefaultAsync(p => p.Id == model.ProfileId);
            if (profile == null)
            {
                return NotFound();
            }

            if (profile.Experiences == null)
            {
                profile.Experiences = new List<Experience>();
            }

            profile.Experiences.Add(new Experience()
            {
                ProfileId = model.ProfileId,
                Value = model.Text
            });

            await _context.SaveChangesAsync();

            return Ok(profile.Experiences);
        }
    }
}
