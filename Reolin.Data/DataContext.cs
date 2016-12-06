using Reolin.Data.EntityFramework.Config;
using Reolin.Data.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Reolin.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        //public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Country> Countries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        public DataContext(): base("Default")
        {

        }
        public DataContext(string connectionString = "Default"): base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(ProfileConfig).Assembly);
        }
    }
}
