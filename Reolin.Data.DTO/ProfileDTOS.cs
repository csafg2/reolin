
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Reolin.Data.DTO
{
    public class RequestRelatedProfile
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Confirmed { get; set; }
        public int SourceId { get; set; }
        public string SourceIcon { get; set; }
    }

    public class ProfileBasicInfoDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int LikeCount { get; set; }
        public string Name { get; set; }
        public bool IsWork { get; set; }
        public string IconUrl { get; set; }
        public double? Long { get; set; }
        public double? Lat { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }
    }

    public class EducationEditDTO
    {
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "ProfileId is required")]
        public int Id { get; set; }
        public string Major { get; set; }
        public string Level { get; set; }
        public string Field { get; set; }
        [Range(1950, 2017)]
        public int GraduationYear { get; set; }
        public string University { get; set; }
    }

    public class RelateCreateModel
    {
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public int RelatedTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class RelatedTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class RelatedProfileDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class ProfileRedisCacheDTO : ProfileByTagDTO
    {
        public int Id { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int LikeCount { get; set; }
        public double? DistanceWithSource { get; set; }
    }

    public class ProfileSearchResult : ProfileInfoDTO
    {
        public double? DistanceWithSource { get; set; }
    }

    public class ProfileInfoDTO : ProfileByTagDTO
    {
        public int? Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsWork { get; set; }
    }

    public class CreateProfileDTO : ProfileByTagDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public int? JobCategoryId { get; set; }
        public int? SubJobCategoryId { get; set; }
    }

    public class ProfileByTagDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}