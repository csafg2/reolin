using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class ImageCategory
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        public List<Image> Images { get; set; }
    }
}