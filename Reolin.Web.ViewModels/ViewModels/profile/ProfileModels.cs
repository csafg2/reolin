#pragma warning disable CS1591

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace Reolin.Web.ViewModels
{
    public class ProfileSearchModel
    {
        [Required(ErrorMessage = "JobCategoryID is required")]
        public int JobCategoryId { get; set; }

        [Required(ErrorMessage = "SubJobCategoryID is required")]
        public int SubJobCategoryId { get; set; }

        
        [Required(ErrorMessage = "SearchTerm is required", AllowEmptyStrings = false)]
        public string SearchTerm { get; set; }


        [Required(ErrorMessage = "SourceLongitude is required")]
        [Range(-180, 180, ErrorMessage = "Longitude is not valid")]
        public double SourceLongitude { get; set; }

        [Required(ErrorMessage = "SourceLatitude is required")]
        [Range(-90, 90, ErrorMessage = "Latitude is not Valid")]
        public double SourceLatitude { get; set; }

        [Range(1, 100000)]
        public int Distance { get; set; }
    }


    public class ProfileAddNetworkModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "ProfileId is not valid")]
        [Required(ErrorMessage = "ProfileId is required")]
        public int ProfielId { get; set; }


        [Required(ErrorMessage = "SocialNetworkId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "SocialnetworkId is required")]
        public int SocialNetworkId { get; set; }

        [Required(ErrorMessage = "Url is Required", AllowEmptyStrings = false)]
        public string Url { get; set; }

        public string Description { get; set; }
    }

    public class ProfileAddSkillModel
    {
        [Required(ErrorMessage = "ProfileId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id is not valid")]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Skillname is required", AllowEmptyStrings = false)]
        public string SkillName { get; set; }
    }


    public class ProfileEditModel
    {
        [Required(ErrorMessage = "ProfileId to be edited")]
        [Range(1, int.MaxValue)]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(ErrorMessage = "City is required", AllowEmptyStrings = false)]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required", AllowEmptyStrings = false)]
        public string Country { get; set; }
    }

    public class SearchProfilesInRangeModel
    {
        [Required(ErrorMessage = "Tag to search is required", AllowEmptyStrings = false)]
        public string Tag { get; set; }
        
        [Required(ErrorMessage = "Search Range is required")]
        [Range(0, 10000)]
        public double SearchRadius { get; set; }


        [Required(ErrorMessage = "SourceLongitude is required")]
        [Range(-180, 180, ErrorMessage = "Longitude is not valid")]
        public double SourceLongitude { get; set; }

        [Required(ErrorMessage = "SourceLatitude is required")]
        [Range(-90, 90, ErrorMessage = "Latitude is not Valid")]
        public double SourceLatitude { get; set; }
    }
    
    public class ProfileCreateModel
    {
        [Required(ErrorMessage = "Profile Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Description text is mandatory", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Latitude is Mandatory")]
        [Range(-90, 90, ErrorMessage = "Latitude is not Valid")]
        public double? Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is Mandatory")]
        [Range(-180, 180, ErrorMessage = "Longitude is not valid")]
        public double? Longitude { get; set; }

        [Required( ErrorMessage = "Phone Number is required", AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "City is required", AllowEmptyStrings = false)]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required", AllowEmptyStrings = false)]
        public string Country { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "Not valid JobCategoryID")]
        [Required(ErrorMessage = "JobCategoryID is required, you can query them first")]
        public int? JobCategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Not valid SubJobCategoryID")]
        [Required(ErrorMessage = "SubJobCategoryID is required")]
        public int? SubJobCategoryId { get; set; }
        public DbGeography GetLocation()
        {
            return GeoHelpers.FromLongitudeLatitude(this.Longitude, this.Latitude, GeoHelpers.Geo_SRID);
        }
    }
}
