using System.Data.Entity.Migrations;

namespace EDF.TradeCapture.Persistence.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuotesState",
                c => new
                    {
                        CorrelationId = c.Guid(nullable: false),
                        TradeId = c.String(nullable: false, maxLength: 30),
                        State = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CorrelationId)
                .Index(t => t.TradeId, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuotesState", new[] { "TradeId" });
            DropTable("dbo.QuotesState");
        }
    }
}
