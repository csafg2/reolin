using System.Collections.Generic;

namespace Reolin.Data.Domain
{

    public class Profile
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public bool Confirmed { get; set; }
        public Address Address { get; set; }
        //public ProfileType Type { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Image> Images { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }

}