using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class ImageCategoryConfig: EntityTypeConfiguration<ImageCategory>
    {
        public ImageCategoryConfig()
        {
            this.HasKey(imc => imc.Id);

            this.HasMany(imc => imc.Images)
                .WithRequired(img => img.ImageCategory)
                .HasForeignKey(img => img.ImageCategoryId);
        }
    }
}
