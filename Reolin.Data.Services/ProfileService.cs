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

        public Task<int> AddProfileImageAsync(int profileId, int categoryId, string subject, string descrption, string imagePath)
        {
            this.Context.Images.Add(new Image()
            {
                ProfileId = profileId,
                Subject = subject,
                Description = descrption,
                Path = imagePath,
                UploadDate = DateTime.Now,
                ImageCategoryId = categoryId
            });

            return this.Context.SaveChangesAsync();
        }

        public Task<int> AddLikeAsync(int senderProfileId, int targetProfileId)
        {
            this.Context.Likes.Add(new Like()
            {
                TargetProfileId = targetProfileId,
                SenderId = senderProfileId
            });

            return Context.SaveChangesAsync();
        }



        public async Task<Profile> CreateWorkAsync(int userId, CreateProfileDTO dto)
        {
            User user = await Context.Users.Include("Profiles").FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"No user with specified Id {userId} found.");
            }

            return await CreateAsync(user, dto, ProfileType.Work);
        }


        public async Task<Profile> CreatePersonalAsync(int userId, CreateProfileDTO dto)
        {
            User user = await Context.Users.Include("Profiles").FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"No user with specified Id {userId} found.");
            }
            if (user.Profiles != null)
            {
                if (user.Profiles.Any(p => p.Type == ProfileType.Personal))
                {
                    throw new InvalidOperationException($"User '{user.UserName}' already has a personal profile");
                }
            }

            return await CreateAsync(user, dto, ProfileType.Personal);
        }

        private async Task<Profile> CreateAsync(User user, CreateProfileDTO dto, ProfileType type)
        {
            if (user.Profiles == null)
            {
                user.Profiles = new List<Profile>();
            }

            Profile result = InstantiateProfile(dto, type);
            user.Profiles.Add(result);

            await Context.SaveChangesAsync();

            return result;
        }


        private Profile InstantiateProfile(CreateProfileDTO dto, ProfileType type)
        {
            return new Profile()
            {
                Type = type,
                Description = dto.Description,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                JobCategoryId = dto.JobCategoryId,
                Address = new Address()
                {
                    City = dto.City,
                    Country = dto.Country,
                    Location = GeoHelpers.FromLongitudeLatitude(dto.Longitude, dto.Latitude)
                }
            };
        }

        public async Task<ProfileInfoDTO> QueryInfoAsync(int id)
        {
            var result = await this.Context.Profiles.Include("Address").FirstOrDefaultAsync(p => p.Id == id);
            return result;
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


        public async Task<int> EditProfile(int profileId, string city, string country, string name)
        {
            int addressCount = await this.Context.Addresses.Where(a => a.Id == profileId)
                .UpdateAsync(a => new Address() { City = city, Country = country });

            int profileCount = await this.Context.Profiles.Where(p => p.Id == profileId)
                            .UpdateAsync(p => new Profile() { Name = name });

            return addressCount + profileCount;
        }

        public Task<List<CommentDTO>> GetLatestComments(int profileId)
        {
            return this.Context
                        .Comments
                            .Where(c => c.ProfileId == profileId)
                            .OrderByDescending(c => c.Date)
                            .Take(10)
                            .Select(c => new CommentDTO()
                            {
                                Body = c.Body,
                                SenderName = c.User.UserName
                            })
                            .ToListAsync();
        }

        public async Task<int> EditEducation(int profileId, EducationEditDTO dto)
        {
            var profile = await this.Context
                .Profiles
                    .Include("Education")
                        .FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null)
            {
                throw new InvalidOperationException($"no profile with id '{profile}' found. ");
            }
            else if (profile.Type == ProfileType.Work)
            {
                throw new InvalidOperationException($"only personal profiles should have edu info");
            }

            return await UpdateOrCreateEducation(profile, dto);
        }

        private Task<int> UpdateOrCreateEducation(Profile profile, EducationEditDTO dto)
        {
            if (profile.Education == null)
            {
                profile.Education = new Education()
                {
                    Field = dto.Field,
                    GraduationYear = dto.GraduationYear,
                    Level = dto.Level,
                    University = dto.University
                };
            }
            else
            {
                Education edu = profile.Education;
                edu.Field = dto.Field;
                edu.GraduationYear = dto.GraduationYear;
                edu.Level = dto.Level;
                edu.University = dto.University;
            }

            return Context.SaveChangesAsync();
        }

        public async Task<int> AddSkill(int profileId, string skill)
        {
            if (string.IsNullOrEmpty(skill))
            {
                throw new ArgumentNullException(nameof(Skill));
            }

            Profile profile = await Context.Profiles.Include("Skills").FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile.Skills.Any(s => s.Name == skill))
            {
                return 0;
            }

            Skill skillItem = await CreateSkill(skill, profile);

            profile.Skills.Add(skillItem);
            return await Context.SaveChangesAsync();
        }

        private async Task<Skill> CreateSkill(string skill, Profile profile)
        {
            Skill skillItem = await Context.Skills.FirstOrDefaultAsync(s => s.Name == skill);
            if (skillItem == null)
            {
                skillItem = new Skill() { Name = skill };
                profile.Skills.Add(skillItem);
            }

            return skillItem;
        }

        public Task<int> AddSocialNetwork(int profileId, int networkId, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            ProfileNetwork network = new ProfileNetwork()
            {
                NetworkId = networkId,
                ProfileId = profileId,
                Url = url
            };

            Context.ProfileNetworks.Add(network);

            return Context.SaveChangesAsync();
        }

        //TODO: move this to ImageCategoryService for god sake.
        public async Task<int> AddImageCategory(int profileId, string name)
        {
            if (await Context.ImageCategories.AnyAsync(i => i.ProfileId == profileId && i.Name == name))
            {
                throw new InvalidOperationException($"user alreadys has {name}.");
            }

            Context.ImageCategories.Add(new ImageCategory()
            {
                Name = name
            });

            return await this.Context.SaveChangesAsync();
        }

        public Task<List<JobCategoryInfoDTO>> QueryJobCategories()
        {
            return this.Context.JobCategories.Select(j => new JobCategoryInfoDTO() { Id = j.Id, Name = j.Name }).ToListAsync();
        }
    }
}
