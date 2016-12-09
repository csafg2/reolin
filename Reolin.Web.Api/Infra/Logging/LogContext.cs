using System.Data.Entity;

namespace Reolin.Web.Api.Infra.Logging
{

    public class LogContext: DbContext
    {
        public LogContext():base("Server=127.0.0.1\\DefaultInstance; Database =LogDb; User Id=sa; Password=123;")
        {

        }

        //public LogContext(string message): base(message)
        //{
        //}

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(l => l.Id);
        }
    }
}