using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{

    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            
            this.HasKey(a => a.Id);
        }
    }
}