using System;
using System.Collections.Generic;

namespace Reolin.Domain
{

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }

}