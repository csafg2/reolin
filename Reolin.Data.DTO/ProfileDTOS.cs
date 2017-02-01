
namespace Reolin.Data.DTO
{
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