
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using System;
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
        /// <returns></returns>
        Task<int> AddProfileImageAsync(int profileId, string imagePath);


        Task<int> AddLikeAsync(int senderUserId, int targetProfileId);

        /// <summary>
        /// Create a new profile entry with the description, for the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<Profile> CreateAsync(int userId, CreateProfileDTO dto);


        Task<ProfileInfoDTO> QueryInfoAsync(int id);

        Task<List<Profile>> GetInRange(string tag, double radius, double sourceLat, double sourceLong);

        Task<List<CreateProfileDTO>> GetRelatedProfiles(int profileId);
    }
}
