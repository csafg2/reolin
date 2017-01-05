
using System;

namespace Reolin.Data.Domain
{
    public class Image
    {
        public int Id { get; set; }
        
        public string Path { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}