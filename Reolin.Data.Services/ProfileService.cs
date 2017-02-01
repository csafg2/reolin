using EntityFramework.Extensions;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Spatial;

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

        public Task<int> AddLikeAsync(int senderUserId, int targetProfileId)
        {
            this.Context.Likes.Add(new Like()
            {
                ProfileId = targetProfileId,
                SenderId = senderUserId
            });

            return Context.SaveChangesAsync();
        }


        public async Task<Profile> CreateAsync(int userId, CreateProfileDTO dto)
        {
            User user = await Context.Users.Include("Profiles").FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"No user with specified Id {userId} found.");
            }

            if (user.Profiles == null)
            {
                user.Profiles = new List<Profile>();
            }

            Profile result = InstantiateProfile(dto);
            user.Profiles.Add(result);

            await Context.SaveChangesAsync();

            return result;
        }

        private Profile InstantiateProfile(CreateProfileDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Profile()
            {
                Description = dto.Description,
                Name = dto.Name,
                Address = new Address()
                {
                    Location = GeoHelpers.FromLongitudeLatitude(dto.Longitude, dto.Latitude)
                }
            };
        }

        public async Task<ProfileInfoDTO> QueryInfoAsync(int id)
        {
            return await this.Context.Profiles.Include("Address").FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Profile>> GetInRange(string tag, double radius, double sourceLat, double sourceLong)
        {
            DbGeography other = GeoHelpers.FromLongitudeLatitude(sourceLong, sourceLat);
            return this.Context.Profiles.Where(p => p.Address.Location.Distance(other) < radius).ToListAsync();
        }
    }

}
