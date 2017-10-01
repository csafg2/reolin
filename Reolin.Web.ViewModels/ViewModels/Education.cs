using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels.ViewModels
{
    public class EducationGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "invalid profileId")]
        public int ProfileId { get; set; }
    }

    public class EducationCreateModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "invalid profileId")]
        public int ProfileId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Level is required")]
        public string Level { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Major is required")]
        public string Major { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field is required")]
        public string Field { get; set; }

        [Range(1970, int.MaxValue, ErrorMessage = "invalid GraduationYear")]
        public int GraduationYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "University is required")]
        public string University { get; set; }
    }
}
