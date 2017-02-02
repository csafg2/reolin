namespace Reolin.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            //string assemblyPath = Assembly.GetExecutingAssembly().Location;
            //string path = Path.Combine(assemblyPath, "/RawSql/InsertTagStoreProcedure.sql");

            //string storeProcedureCommand = File.ReadAllText(path);
            //context.Database.ExecuteSqlCommand(storeProcedureCommand);
        }
    }
}
