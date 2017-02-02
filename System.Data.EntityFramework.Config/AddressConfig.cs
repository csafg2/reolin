using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{

    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            this.HasKey(a => a.Id);

            this.HasMany(a => a.Tags)
                .WithMany(t => t.Addresses)
                .Map(t => t.MapLeftKey("AddressId").MapRightKey("TagId"));
        }
    }
}