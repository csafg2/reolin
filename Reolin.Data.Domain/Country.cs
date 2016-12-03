
using System;

namespace Reolin.Data.Domain
{
    [Obsolete("address class now recognize the city and country by #tag")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}