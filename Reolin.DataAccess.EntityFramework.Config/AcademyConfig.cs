using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{
    public class AcademyConfig: EntityTypeConfiguration<Academy>
    {
        public AcademyConfig()
        {
            this.HasKey(a => a.Id);

            //TODO: complete config for academy class
        }
    }
}
