using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class RelatedTypeConfig: EntityTypeConfiguration<RelatedType>
    {
        public RelatedTypeConfig()
        {
            this.HasKey(rt => rt.Id);

            this.HasMany(rt => rt.Relatedes)
                .WithRequired(r => r.RelatedType)
                .HasForeignKey(r => r.RelatedTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
