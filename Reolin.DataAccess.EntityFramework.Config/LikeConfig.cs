using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{

    public class LikeConfig : EntityTypeConfiguration<Like>
    {
        public LikeConfig()
        {
            this.HasKey(l => l.Id);
        }
    }
}