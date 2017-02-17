using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class RelatedConfig: EntityTypeConfiguration<Related>
    {
        public RelatedConfig()
        {
            this.HasKey(rt => rt.Id);
        }
    }
}
