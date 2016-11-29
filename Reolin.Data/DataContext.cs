using Reolin.Data.EntityFramework.Config;
using Reolin.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Reolin.Data
{
    public class DataContext: DbContext
    {
        public DataContext(): base("Default")
        {
        }

        public DbSet<User> Users { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(ProfileConfig).Assembly);
        }
    }
}
