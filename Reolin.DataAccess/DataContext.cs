using Reolin.DataAccess.Domain;
using Reolin.DataAccess.EntityFramework.Config;
using System.Data.Entity;

namespace Reolin.DataAccess
{
    public class DataContext: DbContext
    {
        
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(ProfileConfig).Assembly);
        }
    }
}
