using Reolin.Data.Domain;
using Reolin.Data.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    /// <summary>
    /// contains the database operations for a profile entity
    /// </summary>
    public interface IProfileService
    {
        Task<int> AddImageCategory(int profileId, string name);

        /// <summary>
        /// Asynchronously add a descriptions string to a profile
        /// </summary>
        /// <param name="profileId">the if of the profile</param>
        /// <param name="description">the description string</param>
        /// <returns></returns>
        Task AddDescriptionAsync(int profileId, string description);

        /// <summary>
        /// Asynchronously attach a list of tags to a profile entity
        /// </summary>
        /// <param name="profileId">the id of a profile that gets the news tags</param>
        /// <param name="tags">tags to be added to the profile</param>
        /// <returns></returns>
        Task AddTagAsync(int profileId, IEnumerable<string> tags);

        /// <summary>
        /// Get an IQuerayable of objects of Profiles wihch can be asynchronously loaded into memroy by calling ToListAsync()
        /// </summary>
        /// <param name="tag">tag text</param>
        /// <returns></returns>
        IQueryable<ProfileByTagDTO> GetByTagAsync(string tag);

        /// <summary>
        /// attachs an imaged to specified profile record
        /// </summary>
        /// <param name="profileId">the id of profile to be update</param>
        /// <param name="imagePath">the patch of image after it`s been saved</param>
        /// <returns></returns>f
        Task<int> AddProfileImageAsync(int profileId, int categoryId, string subject, string descrption, string imagePath);


        Task<int> AddLikeAsync(int senderProfileId, int targetProfileId);

        /// <summary>
        /// Create a new profile entry with the description, for the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<Profile> CreatePersonalAsync(int userId, CreateProfileDTO dto);

        Task<Profile> CreateWorkAsync(int userId, CreateProfileDTO dto);
    

        Task<ProfileInfoDTO> QueryInfoAsync(int id);

        Task<List<ProfileRedisCacheDTO>> GetInRangeAsync(string tag, double radius, double sourceLat, double sourceLong);

        Task<List<Profile>> GetRelatedProfiles(int profileId);

        Task<int> UpdateLocaiton(int profileId, double latitude, double longitude);
        Task<int> EditProfile(int profileId, string city, string country, string name);

        Task<List<CommentDTO>> GetLatestComments(int profileId);

        Task<int> EditEducation(int profileId, EducationEditDTO dto);

        Task<int> AddSkill(int profileId, string skill);

        Task<int> AddSocialNetwork(int profileId, int netowrkId, string url, string description);
        Task<List<JobCategoryInfoDTO>> QueryJobCategories();


        Task<List<ProfileInfoDTO>> SearchByCategoriesTagsAndDistance(int mainCatId, int subCatId, string searchTerm, int distance, double sourceLatitude, double sourceLongitude);
    }
}
