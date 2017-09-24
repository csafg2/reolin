using Reolin.Data.Seeds;
using System.Data.Entity.Migrations;

namespace Reolin.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            context.AddDefaultNetworks();
            //context.AddDefaultJobCategories();
            //context.AddDefaultJobSubCategoies();
        }
    }
}
