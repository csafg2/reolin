
using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IPorofileService
    {
        Task AddDescriptionAsync(int profileId, string description);
        Task AddTagAsync(int profileId, List<string> list);
        Task<IEnumerable<Profile>> GetByTagAsync(string tag);
    }
}
