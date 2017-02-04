using System.Data.Entity;

namespace Reolin.Web.Api.Infra.Logging
{

    public class LogContext : DbContext
    {
        public LogContext() : base("Default")
        {

        }

        public LogContext(string message) : base(message)
        {
        } 

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(l => l.Id);
        }
    }
}