using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;
using System;
using System.Data.Entity;

namespace Reolin.Data.Services
{

    public class ProfileLocationService : ProfileService, IProfileLocationService
    {
        public ProfileLocationService(DataContext context) : base(context) { }

        public event ProfilesLocationRetrievedHandler ProfileRetrieved;

        public Task<List<ProfileRedisCacheDTO>> GetByDistance(string searchTag, double sourceLat, double sourceLong, double radius)
        {
            var source = Context.Addresses.First().Location;
            return this.Context
                            .Profiles.Include("Tags")
                            .Where(p => (p.Address.Location.Distance(source) <= radius)
                                    &&  (p.Tags.Any(t => t.Name.Contains(searchTag))))
                                .Select(p => new ProfileRedisCacheDTO()
                                {
                                    Id = p.Id,
                                    Description = p.Description,
                                    Latitude = p.Address.Location.Latitude,
                                    Longitude = p.Address.Location.Longitude,
                                    Tags = p.Tags.Select(t => new TagDTO() { Id = t.Id, Name = t.Name }),
                                    Name = p.Name
                                }).ToListAsync();
        }
    }
}