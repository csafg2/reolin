using System.ComponentModel.DataAnnotations;
#pragma warning disable CS1591
namespace Reolin.Web.ViewModels
{

    public class AddImageToProfileViewModel
    {
        [Required(ErrorMessage = "ProfileId is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Id is not valid")]
        public int ProfileId { get; set; }

        public int[] TagIds { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
