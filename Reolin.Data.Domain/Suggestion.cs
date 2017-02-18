using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<Tag> Tags { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
