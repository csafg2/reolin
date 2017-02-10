using Reolin.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace System.Data.EntityFramework.Config
{
    public class JobCategoryConfig: EntityTypeConfiguration<JobCategory>
    {
        public JobCategoryConfig()
        {
            this.HasKey(j => j.Id);

            this.HasMany(j => j.Profiles)
                .WithOptional(p => p.JobCategory)
                .HasForeignKey(p => p.JobCategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
