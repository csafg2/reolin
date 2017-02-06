using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }

}