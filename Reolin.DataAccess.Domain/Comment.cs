using System;
using System.Collections.Generic;

namespace Reolin.DataAccess.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }

}