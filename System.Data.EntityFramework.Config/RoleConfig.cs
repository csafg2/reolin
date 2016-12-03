using Reolin.Data.Domain;
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
        }
    }
}
