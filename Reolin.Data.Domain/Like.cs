
namespace Reolin.Data.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}