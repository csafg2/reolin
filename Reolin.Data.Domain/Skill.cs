using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}