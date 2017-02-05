using EntityFramework.Extensions;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using static Reolin.Data.DataContext.StoreProcedures;
using System.Data.SqlClient;

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

        protected DataContext Context
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

        public virtual async Task AddTagAsync(int profileId, IEnumerable<string> tags)
        {
            // foreach tag:
            // 1: check if exists if so then attach it to profileId
            // otherwise create and then attack it to profileId

            List<SqlParameter> tagNames = this.GetTagSqlParams(tags);
            List<Task<int>> operations = new List<Task<int>>();
            foreach (var tagParameter in tagNames)
            {
                operations.Add(new DataContext()
                    .Database
                        .ExecuteSqlCommandAsync(
                                                INSERT_TAG_PROCEDURE,
                                                new SqlParameter("ProfileId", (long)profileId),
                                                new SqlParameter("AddressId", -1),
                                                tagParameter));
            }

            await Task.WhenAll(operations);
        }

        private List<SqlParameter> GetTagSqlParams(IEnumerable<string> tags)
        {
            List<SqlParameter> tagNames = new List<SqlParameter>();
            foreach (var item in tags)
            {
                if (string.IsNullOrEmpty(item))
                {
                    throw new InvalidOperationException("Tags can not have empty text or name");
                }

                tagNames.Add(new SqlParameter("TagName", item));
            }

            return tagNames;
        }

        public IQueryable<ProfileByTagDTO> GetByTagAsync(string tag)
        {
            return this.Context
                    .Profiles
                        .Where(p => p.Tags.Any(t => t.Name.Contains(tag)))
                        .Select(p => new ProfileByTagDTO
                        {
                            Name = p.Name,
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

        public virtual Task<List<ProfileRedisCacheDTO>> GetInRangeAsync(string tag, double radius, double sourceLat, double sourceLong)
        {
            var source = GeoHelpers.FromLongitudeLatitude(sourceLong, sourceLat);
            return this.Context
                            .Profiles.Include("Tags")
                            .Where(p => (p.Address.Location.Distance(source) <= radius)
                                    && (p.Tags.Any(t => t.Name.Contains(tag))))
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

        public Task<List<Profile>> GetRelatedProfiles(int profileId)
        {
            return this._context
                .Profiles
                    .SqlQuery(GET_RELATED_PROFILES_PROCEDURE, new SqlParameter("ProfileId", profileId))
                        .ToListAsync();
        }

        public virtual async Task<int> UpdateLocaiton(int profileId, double latitude, double longitude)
        {
            Profile profile = await this.Context.Profiles.Include("Address").FirstOrDefaultAsync(p => p.Id == profileId);
            profile.Address.Location = GeoHelpers.FromLongitudeLatitude(longitude, latitude);

            return await Context.SaveChangesAsync();
        }
    }
}
