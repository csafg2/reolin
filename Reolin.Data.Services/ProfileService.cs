using EntityFramework.Extensions;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reolin.Data.Services
{
    public class ProfileService : IProfileService
    {
        private DataContext _context;

        public ProfileService(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        private DataContext Context
        {
            get
            {
                return _context;
            }
        }


        public Task AddDescriptionAsync(int profileId, string description)
        {
            return this.Context
                    .Profiles
                        .Where(p => p.Id == profileId)
                            .UpdateAsync(u => new Profile()
                            {
                                Description = description
                            });
        }

        public Task AddTagAsync(int profileId, IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProfileByTagDTO> GetByTagAsync(string tag)
        {
            return this.Context
                    .Profiles
                        .Where(p => p.Tags.Any(t => t.Name.Contains(tag)))
                        .Select(p => new ProfileByTagDTO
                        {
                            Description = p.Description,
                            Latitude = p.Address.Location.Latitude,
                            Longitude = p.Address.Location.Longitude
                        });
        }

        public Task<int> AddProfileImageAsync(int profileId, string imagePath)
        {
            this.Context.Images.Add(new Image() { ProfileId = profileId, Path = imagePath, UploadDate = DateTime.Now });
            return this.Context.SaveChangesAsync();
        }

        public Task<int> AddLikeAsync(int userId, int profileId)
        {
            this.Context.Likes.Add(new Like()
            {
                ProfileId = profileId,
                SenderId = userId
            });
            
            return Context.SaveChangesAsync();
        }
    }
}
