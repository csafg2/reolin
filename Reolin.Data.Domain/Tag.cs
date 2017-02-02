using System.Collections.Generic;

namespace Reolin.Data.Domain
{

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Address> Addresses { get; set; }
        
    }

}