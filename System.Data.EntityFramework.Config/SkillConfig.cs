using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class SkillConfig: EntityTypeConfiguration<Skill>
    {
        public SkillConfig()
        {
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired();
        }
    }
}