#pragma warning disable CS1591

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace Reolin.Web.Api.ViewModels.profile
{
    public class ProfileCreateModel
    {
        [Required(ErrorMessage = "Description text is mandatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Latitude is Mandatory")]
        [Range(-90, 90, ErrorMessage = "Latitude is not Valid")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is Mandatory")]
        [Range(-180, 180, ErrorMessage = "Longitude is not valid")]
        public double Longitude { get; set; }

        public DbGeography GetLocation()
        {
            return GeoHelpers.FromLongitudeLatitude(this.Longitude, this.Latitude, GeoHelpers.Geo_SRID);
        }
    }
}
