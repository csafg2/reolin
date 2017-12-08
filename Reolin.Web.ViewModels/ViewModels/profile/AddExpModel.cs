#pragma warning disable CS1591


using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels
{
    public class AddAboutModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid profileId")]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Invalid Text", AllowEmptyStrings = false)]
        public string Text { get; set; }
    }

    public class AddExpModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid profileId")]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Invalid Text", AllowEmptyStrings = false)]
        public string Text { get; set; }
    }
}
