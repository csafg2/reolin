using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{


    public class TagConfig: EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            this.HasKey(t => t.Id);
        }
    }
}