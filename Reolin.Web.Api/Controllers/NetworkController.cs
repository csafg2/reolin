using EntityFramework.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class RemoveProfileNetwork
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid ProfileId")]
        public int ProfileId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid NetworkId")]
        public int NetworkId { get; set; }
    }

    [EnableCors("AllowAll")]
    public class NetworkController : ControllerBase
    {
        private readonly DataContext _context;
        public NetworkController(DataContext context)
#pragma warning restore CS1591
        {
            this._context = context;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RemoveNetwork(RemoveProfileNetwork model)
        {
            var result = await this._context
                .ProfileNetworks
                .Where(p => p.ProfileId == model.ProfileId && p.NetworkId == model.NetworkId)
                .DeleteAsync();

            return Ok(result);
        }
    }
}
