using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.ViewModels.profile
{
    public class ProfileAddDescriptionModel
    {
        /// <summary>
        /// description string to be saved for the profile
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }


        /// <summary>
        /// the profile Id
        /// </summary>
        [Required(ErrorMessage = "Profile Id is Mandatory")]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }

}
