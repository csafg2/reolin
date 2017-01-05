using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.ViewModels.profile
{
    public class AddImageToProfileViewModel
    {
        [Required(ErrorMessage = "ProfileId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id is not valid")]
        public int ProfileId { get; set; }
    }

}
