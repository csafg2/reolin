using System;
using System.Collections.Generic;

namespace Reolin.DataAccess.Domain
{

    public class Skill
    {
        public int SecondId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }

}