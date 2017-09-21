using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.Data.EntityFramework.Config
{

    public class CommentConfig: EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Body).IsRequired();

            this.HasMany(c => c.Replies)
                .WithOptional(c => c.Parent)
                .HasForeignKey(c => c.ParentId);
        }
    }
}