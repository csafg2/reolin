using Reolin.Data.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class RoleConfig: EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            this.HasKey(r => r.Id);
            this.Property(r => r.Name).IsRequired();
            this.Property(r => r.Name).HasMaxLength(50);

            this.Property(r => r.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName,
                     new IndexAnnotation(new IndexAttribute("IX_ROLE_NAME", 1)
                     {
                         IsUnique = true
                     }));
        }
    }
}
