using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{

    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            this.HasKey(a => a.Id);
        }
    }
}