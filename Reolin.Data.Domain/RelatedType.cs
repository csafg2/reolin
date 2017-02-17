
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class RelatedType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<Related> Relatedes { get; set; }
    }
}
