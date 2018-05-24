using System.Data.Entity.Migrations;
using EDF.TradeCapture.Persistence.DbContext;

namespace EDF.TradeCapture.Persistence.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PersistedTradeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersistedTradeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
