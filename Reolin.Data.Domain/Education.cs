﻿
namespace Reolin.Data.Domain
{
    public class Education
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Major { get; set; }
        public string Field { get; set; }
        public int GraduationYear { get; set; }
        public string University { get; set; }
        public Profile Profile { get; set; }
    }
}