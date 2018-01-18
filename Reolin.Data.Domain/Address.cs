using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace Reolin.Data.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DbGeography Location { get; set; }
        public Profile Profile { get; set; }
        public Academy Academy { get; set; }
    }
}