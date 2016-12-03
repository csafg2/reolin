using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class TagConfig: EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            this.HasKey(t => t.Id);
        }
    }
}