using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class SuggestionCOnfig: EntityTypeConfiguration<Suggestion>
    {
        public SuggestionCOnfig()
        {
            this.HasKey(s => s.Id);

            this.HasMany(S => S.Tags)
                .WithMany(t => t.Suggestions)
                .Map(t => t.MapLeftKey("SuggestionId").MapRightKey("TagId"));
        }
    }
}
