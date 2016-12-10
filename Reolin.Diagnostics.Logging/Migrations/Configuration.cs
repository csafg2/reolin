namespace Reolin.Diagnostics.Logging.Migrations
{
    using System.Data.Entity.Migrations;
    using Web.Api.Infra.Logging;

    internal sealed class Configuration : DbMigrationsConfiguration<LogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LogContext context)
        {
        }
    }
}
