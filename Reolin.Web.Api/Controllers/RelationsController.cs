using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Web.Api.Infra.mvc;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class RelationsController : BaseController
    {
        private readonly DataContext _context;
        public RelationsController(DataContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Move relation to history.
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> MoveToHistory(int relationId)
        {
            var relation = await _context.Relations.FirstOrDefaultAsync(r => r.Id == relationId);
            if (relation == null)
            {
                return NotFound();
            }

            relation.History = true;

            await _context.SaveChangesAsync();
            return Ok();
        }


        /// <summary>
        /// Delete relation
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int relationId)
        {
            var relation = await _context.Relations.FirstOrDefaultAsync(r => r.Id == relationId);
            _context.Relations.Remove(relation);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Get relation requests
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetRelationRequests(int profileId)
        {
            var requests = await _context
                .Relations
                .Where(r => r.TargetProfileId == profileId && r.Confirmed == false)
                .Select(r => new
                {
                    Request = r,
                    SourceIcon = r.SourceProfile.IconUrl,
                    SourceName = r.SourceProfile.Name,
                    SourceIsWork = r.SourceProfile.Type == Data.Domain.ProfileType.Work
                })
                .ToListAsync();

            return Ok(requests);
        }
    }
}
