using System;

namespace Reolin.Data.Domain
{
    [Obsolete("this class should not be used, because address class now uses hashtags")]
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}