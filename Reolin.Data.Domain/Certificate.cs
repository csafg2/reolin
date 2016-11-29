using System;
using System.Collections.Generic;

namespace Reolin.Domain
{

    public class Certificate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<User> Users { get; set; }
        public List<Profile> Profiles { get; set; }        
    }
}