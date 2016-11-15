using System;
using System.Collections.Generic;

namespace Reolin.DataAccess.Domain
{

    public class Address
    {
        public int Id { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Profile Profile { get; set; }
    }

}