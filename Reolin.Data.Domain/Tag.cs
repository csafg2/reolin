using System.Collections.Generic;

namespace Reolin.Data.Domain
{

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Profile> Profiles { get; set; }
        
        public int? ImageId { get; set; }
        public Image Image { get; set; }
    }
}