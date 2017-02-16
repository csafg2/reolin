
using System;

namespace Reolin.Data.Domain
{
    // profile:relate
    //   1:* 
    public class Related
    {
        public int Id { get; set; }
        public bool Confirmed { get; set; }

        public Profile TargetProfile { get; set; }
        public int TargetProfileId { get; set; }

        public int SourceProfileId { get; set; }
        public Profile SourceProfile { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
