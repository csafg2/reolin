
namespace Reolin.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public int UserId { get; set; }
        public Profile LikeProfile { get; set; }
    }
}