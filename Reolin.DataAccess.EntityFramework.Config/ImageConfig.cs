using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{

    public class ImageConfig: EntityTypeConfiguration<Image>
    {
        public ImageConfig()
        {
            this.HasKey(i => i.Id);
        }
    }
}