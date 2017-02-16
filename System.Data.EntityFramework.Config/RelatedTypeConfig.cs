using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class RelatedTypeConfig: EntityTypeConfiguration<Related>
    {
        public RelatedTypeConfig()
        {
            this.HasKey(rt => rt.Id);
        }
    }
}
