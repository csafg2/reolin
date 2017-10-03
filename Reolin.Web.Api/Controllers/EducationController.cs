#pragma warning disable CS1591
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Web.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var profile = await _context
                .Profiles
                .Include(p => p.Educations)
                .FirstOrDefaultAsync(p => p.Id == model.ProfileId);

            if (profile.Educations == null)
            {
                profile.Educations = new List<Education>();
            }

            profile.Educations.Add(new Education()
            {
                Field = model.Field,
                GraduationYear = model.GraduationYear,
                Level = model.Level,
                Major = model.Major,
                University = model.University
            });

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
            var edu = await _context.Educations.Where(p => p.Id == model.ProfileId).ToListAsync();

            return Ok(edu);
        }


        /// <summary>
        /// modify education info of the profile
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> EditEducation(EducationEditDTO dto)
        {
            var edu = await _context.Educations.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (edu == null)
            {
                return NotFound();
            }

            edu.Level = dto.Level;
            edu.Major = dto.Major;
            edu.GraduationYear = dto.GraduationYear;
            edu.University = dto.University;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}