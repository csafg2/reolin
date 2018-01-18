using EntityFramework.Extensions;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Reolin.Data.DataContext.StoreProcedures;

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
            foreach (var tagParameter in tagNames)
            {
                await this.Context.Database.ExecuteSqlCommandAsync(
                                                INSERT_TAG_PROCEDURE,
                                                new SqlParameter("ProfileId", (long)profileId),
                                                tagParameter);
            }

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

        public IQueryable<ProfileSearchResult> GetByTagAsync(string tag)
        {
            return this.Context
                    .Profiles
                        .Where(p => p.Tags.Any(t => t.Name.Contains(tag)))
                        .Select(p => new ProfileSearchResult
                        {
                            Id = p.Id,
                            City = p.Address.City,
                            Country = p.Address.Country,
                            Description = p.Description,
                            Latitude = p.Address.Location.Latitude,
                            Longitude = p.Address.Location.Longitude,
                            Name = p.Name,
                            Icon = p.IconUrl
                        });
        }

        public async Task<int> AddProfileImageAsync(int profileId, int categoryId, string subject, string descrption, string imagePath, int[] tagsIds)
        {
            var tags = await Context.Tags.Where(t => tagsIds.Contains(t.Id)).ToListAsync();
            this.Context.Images.Add(new Image()
            {
                ProfileId = profileId,
                Subject = subject,
                Description = descrption,
                Tags = tags,
                Path = imagePath,
                UploadDate = DateTime.Now,
                ImageCategoryId = categoryId
            });

            return await this.Context.SaveChangesAsync();
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
            //User user = await Context.Users.Include("Profiles").FirstOrDefaultAsync(u => u.Id == userId);
            User user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.Profiles = new List<Profile>();
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

            Profile result = await InstantiateProfile(dto, type);
            user.Profiles.Add(result);

            await Context.SaveChangesAsync();

            return result;
        }


        private async Task<Profile> InstantiateProfile(CreateProfileDTO dto, ProfileType type)
        {
            return new Profile()
            {
                Type = type,
                Description = dto.Description,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                JobCategories = await this.Context.JobCategories.Where(j => j.Id == dto.JobCategoryId || j.Id == dto.SubJobCategoryId).ToListAsync(),
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
            var result = await this.Context
                .Profiles
                .Include("Address")
                .FirstOrDefaultAsync(p => p.Id == id);

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

        //public async Task<int> EditEducation(int profileId, EducationEditDTO dto)
        //{
        //    var profile = await this.Context
        //        .Profiles
        //            .Include("Education")
        //                .FirstOrDefaultAsync(p => p.Id == profileId);

        //    if (profile == null)
        //    {
        //        throw new InvalidOperationException($"no profile with id '{profile}' found. ");
        //    }
        //    else if (profile.Type == ProfileType.Work)
        //    {
        //        throw new InvalidOperationException($"only personal profiles should have edu info");
        //    }

        //    return await UpdateOrCreateEducation(profile, dto);
        //}

        //private Task<int> UpdateOrCreateEducation(Profile profile, EducationEditDTO dto)
        //{
        //    if (profile.Education == null)
        //    {
        //        profile.Education = new Education()
        //        {
        //            Field = dto.Field,
        //            GraduationYear = dto.GraduationYear,
        //            Level = dto.Level,
        //            University = dto.University
        //        };
        //    }
        //    else
        //    {
        //        Education edu = profile.Education;
        //        edu.Field = dto.Field;
        //        edu.GraduationYear = dto.GraduationYear;
        //        edu.Level = dto.Level;
        //        edu.University = dto.University;
        //    }

        //    return Context.SaveChangesAsync();
        //}

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

        public Task<int> AddSocialNetwork(int profileId, int networkId, string url, string desciption)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            ProfileNetwork network = new ProfileNetwork()
            {
                NetworkId = networkId,
                ProfileId = profileId,
                Url = url,
                Description = desciption
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
                ProfileId = profileId,
                Name = name
            });

            return await this.Context.SaveChangesAsync();
        }

        public Task<List<JobCategoryInfoDTO>> QueryJobCategories()
        {
            return this.Context.JobCategories.Select(j => new JobCategoryInfoDTO() { Id = j.Id, Name = j.Name, IsSubCategory = j.IsSubCategory }).ToListAsync();
        }

        public Task<List<ProfileSearchResult>> SearchBySubCategoryTagsAndDistance(int? subCatId, string searchTerm, double sourceLatitude, double sourceLongitude, int distance = 5000)
        {
            var query = this.Context.Profiles.AsQueryable();

            if (subCatId != null)
            {
                query = query.Where(p => p.JobCategories.Any(j => j.Id == subCatId));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p =>
                    p.JobCategories.Any(j => j.Id == subCatId)
                    ||
                    p.Name.Contains(searchTerm)
                    ||
                    p.Tags.Any(t => t.Name.Contains(searchTerm)));
            }

            var sourceLocation = GeoHelpers.FromLongitudeLatitude(sourceLongitude, sourceLatitude);
            return query
                .Where(p => p.Address.Location.Distance(sourceLocation) < distance)
                .Select(p => new ProfileSearchResult()
                {
                    Id = p.Id,
                    City = p.Address.City,
                    Country = p.Address.Country,
                    Description = p.Description,
                    Latitude = p.Address.Location.Latitude,
                    Longitude = p.Address.Location.Longitude,
                    Name = p.Name,
                    LikeCount = p.ReceivedLikes.Count,
                    DistanceWithSource = p.Address.Location.Distance(sourceLocation)
                })
                .Take(20)
                .ToListAsync();
        }

        public Task<List<ProfileSearchResult>> SearchByCategoriesTagsAndDistance(int? mainCatId, int? subCatId, string searchTerm, double sourceLatitude, double sourceLongitude, int distance = 5000)
        {
            Expression<Func<Profile, bool>> filter = p => true;
            if (mainCatId != null && subCatId != null)
            {
                filter = p => p.JobCategories.Any(j => j.Id == mainCatId) && p.JobCategories.Any(j => j.Id == subCatId);
            }
            else if (mainCatId != null)
            {
                filter = p => p.JobCategories.Any(j => j.Id == mainCatId);
            }
            else if (subCatId != null)
            {
                filter = p => p.JobCategories.Any(j => j.Id == subCatId);
            }

            var query = this.Context.Profiles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Tags.Any(t => t.Name.Contains(searchTerm)));
            }

            DbGeography sourceLocation = GeoHelpers.FromLongitudeLatitude(sourceLongitude, sourceLatitude);
            return
                query
                .Where(filter)
                .Where(p => p.Address.Location.Distance(sourceLocation) < distance)
                .Select(p => new ProfileSearchResult()
                {
                    Id = p.Id,
                    City = p.Address.City,
                    Country = p.Address.Country,
                    Description = p.Description,
                    Latitude = p.Address.Location.Latitude,
                    Longitude = p.Address.Location.Longitude,
                    Name = p.Name,
                    LikeCount = p.ReceivedLikes.Count,
                    DistanceWithSource = p.Address.Location.Distance(sourceLocation)
                })
                .ToListAsync();
        }

        private Task<List<ProfileSearchResult>> Search(Expression<Func<Profile, bool>> categoryPredicate, string searchTerm, double sourceLatitude, double sourceLongitude, int distance)
        {
            DbGeography sourceLocation = GeoHelpers.FromLongitudeLatitude(sourceLongitude, sourceLatitude);
            return this.Context.
                Profiles
                .Where(categoryPredicate)
                .Where(p => p.Name.Contains(searchTerm) || p.Tags.Any(t => t.Name.Contains(searchTerm)))
                .Where(p => p.Address.Location.Distance(sourceLocation) < distance)
                .Select(p => new ProfileSearchResult()
                {
                    Id = p.Id,
                    City = p.Address.City,
                    Country = p.Address.Country,
                    Description = p.Description,
                    Latitude = p.Address.Location.Latitude,
                    Longitude = p.Address.Location.Longitude,
                    Name = p.Name,
                    DistanceWithSource = p.Address.Location.Distance(sourceLocation)
                })
                .ToListAsync();
        }

        public Task<int> AddRelate(int sourceId, int targetId, DateTime date, string description, int relatedTypeId)
        {
            this.Context.Relations.Add(new Related()
            {
                SourceProfileId = sourceId,
                TargetProfileId = targetId,
                RelatedTypeId = relatedTypeId,
                Date = date,
                Description = description
            });

            return this.Context.SaveChangesAsync();
        }

        public Task<ProfileBasicInfoDTO> GetBasicInfo(int profileId)
        {
            var profile = Context
                .Profiles
                .Where(p => p.Id == profileId)
                .Select(p => new ProfileBasicInfoDTO()
                {
                    Id = p.Id,
                    City = p.Address.City,
                    Country = p.Address.Country,
                    LikeCount = p.ReceivedLikes.Count,
                    Name = p.Name,
                    IsWork = p.Type == ProfileType.Work,
                    IconUrl = p.IconUrl,
                    Lat = p.Address.Location.Latitude,
                    Long = p.Address.Location.Longitude,
                    Address = p.Address.AddressString,
                    AboutMe = p.AboutMe
                }).FirstOrDefaultAsync();

            return profile;

        }

        public async Task<List<TagDTO>> GetTags(int profileId)
        {
            var profile =
                await Context
                .Profiles
                .Include("Tags")
                .FirstOrDefaultAsync(p => p.Id == profileId);

            return profile.Tags.Select(t => new TagDTO() { Id = t.Id, Name = t.Name }).ToList();
        }

        public async Task<string> GetPhoneNumbers(int id)
        {
            var profile = await this.Context.Profiles.FirstOrDefaultAsync(p => p.Id == id);

            return profile.PhoneNumber;
        }

        public Task<int> AddRelatedType(int profileId, string relatedType)
        {
            this.Context.RelatedTypes.Add(new RelatedType()
            {
                ProfileId = profileId,
                Type = relatedType
            });

            return Context.SaveChangesAsync();
        }

        public Task<List<RelatedTypeDTO>> GetRelatedTypes(int profileId)
        {
            return this.Context
                .RelatedTypes
                .Where(rt => rt.ProfileId == profileId)
                    .Select(rt => new RelatedTypeDTO() { Id = rt.Id, Name = rt.Type })
                        .ToListAsync();
        }

        //TODO:  candidate to be moved to Separat service
        public Task<List<ImageDTO>> GetImages(int profileId)
        {
            return this.Context
                .Images
                .Where(i => i.ProfileId == profileId)
                .Select(i => new ImageDTO()
                {
                    Id = i.Id,
                    Category = i.ImageCategory.Name,
                    Description = i.Description,
                    Subject = i.Subject,
                    Path = i.Path
                }).ToListAsync();
        }

        public Task<List<RequestRelatedProfile>> GetRequestRelatedProfiles(int profileId)
        {
            return this.Context.Relations.Where(r => r.TargetProfileId == profileId)
                .Select(r => new RequestRelatedProfile()
                {
                    Date = r.Date,
                    SourceId = r.SourceProfileId,
                    SourceIcon = r.SourceProfile.IconUrl,
                    Description = r.Description,
                    Type = r.RelatedType.Type,
                    Name = r.SourceProfile.Name,
                    Confirmed = r.Confirmed,
                    Id = r.Id
                })
                .ToListAsync();
        }

        public Task<int> AddCertificateAsync(int profileId, int year, string description)
        {
            Certificate c = new Certificate()
            {
                Description = description,
                ProfileId = profileId,
                Year = year
            };

            this.Context.Certificates.Add(c);

            return Context.SaveChangesAsync();
        }

        public Task<List<Certificate>> GetCertificates(int profileId)
        {
            return this.Context.Certificates.Where(c => c.ProfileId == profileId).ToListAsync();
        }

        public Task<int> DeleteRelationRequest(int id)
        {
            return this.Context.Relations.Where(r => r.Id == id).DeleteAsync();
        }

        public Task<int> ConfirmRelationRequest(int id)
        {
            return this.Context
                .Relations
                .Where(r => r.Id == id)
                .UpdateAsync(r => new Related()
                {
                    Confirmed = true
                });
        }

        public Task<Address> GetLocation(int id)
        {
            return this.Context.Addresses.FirstAsync(a => a.Id == id);
        }
    }
}
