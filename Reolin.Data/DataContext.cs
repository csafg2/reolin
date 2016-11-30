using Reolin.Data.EntityFramework.Config;
using Reolin.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reolin.Data
{
    public static class DbConfiguration
    {
        public static string CurrentConnectionString
        {
            get
            {
                if (true)
                {

                }
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("project.json")
                .Build();

                return builder["ConnectionStrings:Default"];
            }
        }

        
    }

    public class DataContext: DbContext
    {
        public DataContext(): base(@"Server=127.0.0.1\DefaultInstance;
                                        Database = ReolinDb; 
                                        User Id=sa;
                                        Password=123;")
        { }

        public DbSet<User> Users { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(ProfileConfig).Assembly);
        }
    }
}
