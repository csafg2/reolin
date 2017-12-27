#pragma warning disable CS1591
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{

    [EnableCors("AllowAll")]
    public class SocialNetworkController : Controller
    {
        private DataContext _context;

        public SocialNetworkController(DataContext context)
        {
            this._context = context;
        }

#pragma warning restore CS1591

        /// <summary>
        /// لیست شبکه های اجتماعی با جزئیات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> List()
        {
            return Ok(await this._context.SocialNetworks.Select(n => new
            {
                Id = n.Id,
                Name = n.Name,
                IconPath = n.IconPath
            }).ToListAsync());
        }

        /// <summary>
        /// لیست شبکه های اجتماعی با جزئیات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> GetProfileNetworks(int profileId)
        {
            var networks = await _context
                        .Profiles
                            .Where(p => p.Id == profileId)
                                .SelectMany(p => p.Networks)
                                .Select(n => new
                                {
                                    Network = n.Netowrk,
                                    Url = n.Url,
                                    Description = n.Description
                                })
                                .ToListAsync();
            
            return Ok(networks);
        }
    }
}