#pragma warning disable CS1591
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Reolin.Web.Api.Controllers
{

    public class CityModel
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class GeoServiceController : Controller
    {
        private Dictionary<string, List<CityModel>> _countries { get; set; }


        public GeoServiceController()
        {
            _countries = new Dictionary<string, List<CityModel>>();
            _countries["iran"] = new List<CityModel>();
            _countries["afghanistan"] = new List<CityModel>();

            _countries["iran"].Add(new CityModel() { Name = "Qom", Latitude = 34.65001548, Longitude = 50.95000606 });
            _countries["iran"].Add(new CityModel() { Name = "Tehran", Latitude = 35.67194277, Longitude = 51.42434403 });


            _countries["afghanistan"].Add(new CityModel() { Name = "Baghlan", Latitude = 36.13933026, Longitude = 68.69925858 });
            _countries["afghanistan"].Add(new CityModel() { Name = "Kabul", Latitude = 35.67194277, Longitude = 51.42434403 });

        }

        [Route("[controller]/[action]")]
        [HttpGet]
        public IActionResult Cities(string country)
        {
            return Ok(_countries[country]);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Countries()
        {
            return Ok(_countries.Keys);
        }
    }
}
