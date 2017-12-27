using System;
using System.ComponentModel.DataAnnotations;

namespace Reolin.Data.DTO
{
    public class SuggestionCreateModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string FilePath { get; set; }
        public int ProfileId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
