using System;
using System.Collections.Generic;

namespace Reolin.DataAccess.Domain
{

    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        //public City City { get; set; }
        //public Country Country { get; set; }
        //public Location Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public User User { get; set; }
        public Profile Profile { get; set; }
        public Academy Academy { get; set; }
        public List<Tag> Tags { get; set; }
    }

}