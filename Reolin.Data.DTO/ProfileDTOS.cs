﻿
namespace Reolin.Data.DTO
{
    public class RelatedProfileDTO 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

    public class ProfileInfoDTO: ProfileByTagDTO
    {
    }

    public class CreateProfileDTO : ProfileByTagDTO
    {
    }

    public class ProfileByTagDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}