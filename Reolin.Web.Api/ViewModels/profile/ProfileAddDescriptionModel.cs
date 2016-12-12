using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.ViewModels.profile
{
    public class ProfileAddDescriptionModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }

}
