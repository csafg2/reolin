using Reolin.DataAccess.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Reolin.DataAccess.EntityFramework.Config
{

    public class CommentConfig: EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            this.HasKey(c => c.Id);

        }
    }
}