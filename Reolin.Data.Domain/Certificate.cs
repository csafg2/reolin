using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{

    public class Certificate
    {
        public int Id { get; set; }
        public int Year { get; set; }
        //public string Name { get; set; }
        public string Description { get; set; }
        //public string Url { get; set; }
        //public List<User> Users { get; set; }
        //public List<Profile> Profiles { get; set; }        
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        public string ImageUrl { get; set; }
    }
}