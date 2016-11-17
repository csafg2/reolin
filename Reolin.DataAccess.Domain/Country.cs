
using System;

namespace Reolin.DataAccess.Domain
{
    [Obsolete("address class now recognize the city and country by #tag")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}