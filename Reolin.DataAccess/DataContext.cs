using Reolin.DataAccess.EntityFramework.Config;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Reolin.DataAccess
{
    public class DataContext: DbContext
    {
        public DataContext(): base("Default")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(ProfileConfig).Assembly);
        }
    }
}
