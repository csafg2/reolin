using Reolin.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class AcademyConfig: EntityTypeConfiguration<Academy>
    {
        public AcademyConfig()
        {
            this.HasKey(a => a.Id);

            //TODO: complete config for academy class
            this.HasRequired(a => a.Address)
                .WithOptional(ad => ad.Academy);

        }
    }

}
