using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels.ViewModels
{
    public class CreateCommentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Comment body is required")]
        public string Body { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "invalid profile id")]
        public int ProfileId { get; set; }
    }

    public class CommentReplyModel : CreateCommentModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "invalid comment id")]
        public int CommentId { get; set; }
    }

}
