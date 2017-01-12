using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class AcademyConfig: EntityTypeConfiguration<Academy>
    {
        public AcademyConfig()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.Name).IsRequired();

            this.HasRequired(a => a.Address)
                .WithOptional(ad => ad.Academy);

        }
    }
}