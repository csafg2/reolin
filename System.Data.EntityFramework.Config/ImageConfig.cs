﻿using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{
    public class ImageConfig: EntityTypeConfiguration<Image>
    {
        public ImageConfig()
        {
            this.HasKey(i => i.Id);

            this.HasMany(im => im.Tags)
                .WithMany(t => t.Images)
                .Map(t => t.MapLeftKey("ImageId").MapRightKey("TagId").ToTable("ImageTag"));
        }
    }
}