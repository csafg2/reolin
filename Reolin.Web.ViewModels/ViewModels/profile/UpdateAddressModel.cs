using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UpdateAddressModel
    {
        [Required(ErrorMessage = "ProfileId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProfileId is not valid")]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Latitude is Required")]
        [Range(-90, 90, ErrorMessage = "Value must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is Required")]
        [Range(-180, 180, ErrorMessage = "Value must be between -180 and 180")]
        public double? Longitude { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
