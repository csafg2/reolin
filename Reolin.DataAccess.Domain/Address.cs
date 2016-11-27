using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Reolin.DataAccess.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DbGeography Location { get; set; }
        public User User { get; set; }
        public Profile Profile { get; set; }
        public Academy Academy { get; set; }
        public List<Tag> Tags { get; set; }

        [NotMapped]
        public const int Geo_SRID = 4326;
    }
}