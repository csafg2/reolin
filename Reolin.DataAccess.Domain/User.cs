using System.Collections.Generic;

namespace Reolin.DataAccess.Domain
{
    /// <summary>
    /// this class holds central user states
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public List<Tag> Tags { get; set; }
        public Address Address { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Profile> Profiles { get; set; }

        // thumbs up that user has done.
        public List<Like> Likes { get; set; }
        
        public bool Confirmed { get; set; }
    }
}