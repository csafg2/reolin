
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProfileNetwork> ProfileNetworks { get; set; }
    }

    public class ProfileNetwork
    {
        public int Id { get; set; }

        public Profile Profile { get; set; }
        public int ProfileId { get; set; }


        public SocialNetwork Netowrk { get; set; }
        public int NetworkId { get; set; }

        public string Url { get; set; }
    }

}
