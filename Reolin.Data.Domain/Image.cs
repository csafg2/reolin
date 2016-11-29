
namespace Reolin.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}