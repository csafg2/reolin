
using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    /// <summary>
    /// contains the database operations for a profile entity
    /// </summary>
    public interface IPorofileService
    {
        /// <summary>
        /// Asynchronously add a descriptions string to a profile
        /// </summary>
        /// <param name="profileId">the if of the profile</param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task AddDescriptionAsync(int profileId, string description);

        /// <summary>
        /// Asynchronously attach a list of tags to a profile entity
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task AddTagAsync(int profileId, IEnumerable<string> tags);

        /// <summary>
        /// Get a list of profiles which are connected to a pofile entity
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        IQueryable<Profile> GetByTagAsync(string tag);
    }
}
