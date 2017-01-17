using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class PhoneNumberConfig: EntityTypeConfiguration<PhoneNumber>
    {
        public PhoneNumberConfig()
        {
            this.HasKey(p => p.ProfileId);
        }
    }
}
