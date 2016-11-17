using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{

    public class SkillConfig: EntityTypeConfiguration<Skill>
    {
        public SkillConfig()
        {
            this.HasKey(s => s.Id);
        }
    }
}