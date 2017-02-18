using Reolin.Data.Domain;
using Reolin.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface ISuggestionService
    {
        Task<Suggestion> AddSuggestion(SuggestionCreateModel model);
        Task<int> AddTag(int suggestionId, string tag);
        Task<List<Suggestion>> GetSuggestions(int profileId);
    }
}
