
namespace Reolin.Data.Domain
{
    public class Like
    {
        public int Id { get; set; }

        public User Sender { get; set; }
        public int SenderId { get; set; }

        public Profile TargetProfile { get; set; }
        public int TargetProfileId { get; set; }
    }
}