using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class EducationConfig : EntityTypeConfiguration<Education>
    {
        public EducationConfig()
        {
            this.HasKey(e => e.Id);
        }
    }
}
