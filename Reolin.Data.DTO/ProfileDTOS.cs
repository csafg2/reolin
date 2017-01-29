﻿
namespace Reolin.Data.DTO
{
    public class CreateProfileDTO : ProfileByTagDTO { }

    public class ProfileByTagDTO
    {
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}