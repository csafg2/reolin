using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{

    public class LikeConfig : EntityTypeConfiguration<Like>
    {
        public LikeConfig()
        {
            this.HasKey(l => l.Id);
        }
    }
}