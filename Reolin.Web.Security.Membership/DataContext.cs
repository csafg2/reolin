using System.Data.Entity;

namespace Reolin.Web.Security.Membership
{
    public class DataContext : DbContext
    {
        public DataContext() : base("default")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
