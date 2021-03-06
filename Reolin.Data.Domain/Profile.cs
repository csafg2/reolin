using Reolin.Data.DTO;
using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class Experience
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public string Value { get; set; }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Confirmed { get; set; }
        public Address Address { get; set; }
        public ProfileType Type { get; set; }
        public List<Tag> Tags { get; set; }
        //public List<Like> Likes { get; set; }
        public List<Like> ReceivedLikes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Image> Images { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ImageCategory> ImageCategories { get; set; }
        public List<JobCategory> JobCategories { get; set; }
        public List<Related> Relatedes { get; set; }
        public List<Related> RelatedTos { get; set; }
        public List<RelatedType> RelatedTypes { get; set; }
        public List<Suggestion> Suggestions { get; set; }
        public List<Education> Educations { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string Postal { get; set; }
        public string Fax { get; set; }
        public string IconUrl { get; set; }
        public string AboutMe { get; set; }
        public string PersonalPhone { get; set; }
        public string Major { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<ProfileNetwork> Networks { get; set; }
        public int? Radius { get; set; }

        public static implicit operator ProfileInfoDTO(Profile source)
        {
            return new ProfileInfoDTO()
            {
                Id = source.Id,
                City = source.Address?.City,
                Country = source.Address?.Country,
                Name = source.Name,
                Description = source.Description,
                Latitude = source.Address?.Location.Latitude,
                Longitude = source.Address?.Location.Longitude,
                IsWork = source.Type == ProfileType.Work
            };
        }
    }
}