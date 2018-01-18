using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Web.Api.Infra.mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class SkillController : BaseController
    {
        private readonly DataContext _context;

        public SkillController(DataContext context)
        {
            this._context = context;
        }


        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            await _context.Skills.Where(i => i.Id == id).DeleteAsync();
            return Ok();
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Edit(int id, string value)
        {
            await _context.Skills.Where(i => i.Id == id).UpdateAsync(s => new Skill()
            {
                Name = value
            });

            return Ok();
        }
    }
}
