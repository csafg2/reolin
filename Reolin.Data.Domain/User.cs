using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reolin.Data.Domain
{
    /// <summary>
    /// this class holds central user states
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        [RegularExpression(pattern: @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public List<Tag> Tags { get; set; }
        //public Address Address { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Profile> Profiles { get; set; }

        // thumbs up s that user has done.
        public List<Like> Likes { get; set; }
        public List<Role> Roles { get; set; }
        public bool Confirmed { get; set; }

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}