using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.Models
{
    public class SearchSuggestionModel
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Query is required.")]
        public string Query { get; set; }
        
        public int? SubCategoryId { get; set; }

        public double SourceLat { get; set; }
        public double SountLong { get; set; }

    }
}
