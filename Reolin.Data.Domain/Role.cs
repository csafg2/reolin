
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public static Role Default()
        {
            return new Role() { Name = "default" };
        }
    }
}
