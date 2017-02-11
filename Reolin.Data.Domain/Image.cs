
using System;
using System.Collections.Generic;

namespace Reolin.Data.Domain
{

    public class Image
    {
        public int Id { get; set; }
        
        public string Subject { get; set; }
        public string Description { get; set; }

        public string Path { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        public DateTime UploadDate { get; set; }
        public List<Tag> Tags { get; set; }

        public int ImageCategoryId { get; set; }
        public ImageCategory ImageCategory { get; set; }
    }
}