using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.Models
{
    public class OptionalSearch
    {
        public int? SubJobCategoryId { get; set; } 
        public int? JobCategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Search term is required")]
        public string SearchTerm { get; set; }
        public int SourceLat { get; set; }
        public int SoruceLong { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "invalid distance")]
        public double Distance { get; set; }
    }

    public class SearchByNameQuery
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "name must be specified")]
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Longitude { get; set; }
        [Range(1, 200000, ErrorMessage = "Radius must be in 1-200,000")]
        public int Radius { get; set; }
    }
}
