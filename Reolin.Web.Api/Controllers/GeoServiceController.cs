using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Reolin.Data;
using System.Linq;
using System.Data.Entity;

namespace GeoService.Controllers
{
    #pragma warning disable CS1591
    public class GeoServiceController : Controller
    {
        private DataContext _context;

        public GeoServiceController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("/Countries")]
        public async Task<IActionResult> Countries()
        {
            var data = await _context.GeoInfos.Select(c => c.Country).Distinct().ToListAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("/{country}/cities")]
        public async Task<IActionResult> Cities(string country)
        {
            var data = await _context.GeoInfos.Where(g => g.Country == country)
                .Select(c => new
                {
                    Name = c.City,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}

#pragma warning restore CS1591