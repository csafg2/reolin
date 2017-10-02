#pragma warning disable CS1591
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Web.ViewModels.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class EducationController : ControllerBase
    {
        private readonly DataContext _context;
        public EducationController(DataContext context)
#pragma warning restore CS1591
        {
            this._context = context;
        }

        /// <summary>
        /// Create new Education record for this profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> Create(EducationCreateModel model)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == model.ProfileId);
            profile.Education = new Education()
            {
                Field = model.Field,
                GraduationYear = model.GraduationYear,
                Level = model.Level,
                Major = model.Major,
                University = model.University
            };

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Get profile`s education info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> Get(EducationGetModel model)
        {
            var edu = await _context.Educations.FirstOrDefaultAsync(p => p.Id == model.ProfileId);


            return Ok(edu);
        }
    }
}