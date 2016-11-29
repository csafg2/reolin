using System;
using System.Collections.Generic;

namespace Reolin.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

}