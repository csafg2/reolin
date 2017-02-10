using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class SocialNetworkConig: EntityTypeConfiguration<SocialNetwork>
    {
        public SocialNetworkConig()
        {
            this.HasKey(scn => scn.Id);

            this.HasMany(scn => scn.ProfileNetworks)
                .WithRequired(pn => pn.Netowrk)
                .HasForeignKey(pn => pn.NetworkId)
                .WillCascadeOnDelete(false);
        }
    }
}