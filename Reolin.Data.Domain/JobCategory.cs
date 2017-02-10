
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public string Description { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
