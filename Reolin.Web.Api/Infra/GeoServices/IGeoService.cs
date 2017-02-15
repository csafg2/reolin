
using System;

namespace Reolin.Web.Api.Infra.GeoServices
{
    public class FakeGeoService : IGeoService
    {
        public GeoInfo GetGeoInfo(string cityName, string countryName)
        {
            return new GeoInfo() { Latitude = 5, Longitude = 4 };
        }
    }

    public struct GeoInfo
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public interface IGeoService
    {
        GeoInfo GetGeoInfo(string cityName, string countryName);
    }
}
