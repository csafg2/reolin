
using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IPorofileService
    {
        Task AddDescriptionAsync(int profileId, string description);
        Task AddTagAsync(int profileId, IEnumerable<string> tags);
        IQueryable<Profile> GetByTagAsync(string tag);
    }
}
