using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Reolin.Data.Services
{
    public class SuggestionService : ISuggestionService
    {
        private DataContext _context;

        public SuggestionService(DataContext context)
        {
            this._context = context;
        }


        private DataContext Context
        {
            get
            {
                return _context;
            }
        }

        public async Task<int> AddTag(int suggestionId, string tag)
        {
            Tag tagItem = await this.Context.Tags.FirstOrDefaultAsync(t => t.Name == tag);

            if (tagItem == null)
            {
                tagItem = new Tag() { Name = tag };
                Context.Tags.Add(tagItem);
                await Context.SaveChangesAsync();
            }

            var suggestion = this.Context.Suggestions.Include(s => s.Tags).First(s => s.Id == suggestionId);
            suggestion.Tags.Add(tagItem);
            return await Context.SaveChangesAsync();
        }

        public async Task<Suggestion> AddSuggestion(SuggestionCreateModel model)
        {
            var suggestion = new Suggestion()
            {
                ProfileId = model.ProfileId,
                DateCreated = DateTime.Now,
                From = model.From,
                To = model.To,
                Title = model.Title,
                Description = model.Description,
                Image = model.FilePath
            };

            this.Context.Suggestions.Add(suggestion);

            await Context.SaveChangesAsync();
            return suggestion;
        }
    }
}
