
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reolin.Data.DTO
{
    
    public class EducationEditDTO
    {
        [Required(ErrorMessage = "ProfileId is required")]
        [Range(1, int.MaxValue)]
        public int ProfileId { get; set; }
        public string Level { get; set; }
        public string Field { get; set; }
        [Range(1950, 2017)]
        public int GraduationYear { get; set; }
        public string University { get; set; }
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

    
    public class ProfileRedisCacheDTO: ProfileByTagDTO
    {
        public int Id { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }

    public class ProfileSearchResult : ProfileInfoDTO
    {
        public double? DistanceWithSource { get; set; }
    }

    public class ProfileInfoDTO: ProfileByTagDTO
    {
        public int? Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
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