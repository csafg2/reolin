
using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels.ViewModels.User
{
    public class CommentCreateModel
    {
        
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        [Required(ErrorMessage = "Message body is required", AllowEmptyStrings = false)]
        public string Message { get; set; }

    }
}
