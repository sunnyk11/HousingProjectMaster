namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableopportunity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opportunities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        OpportunityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Opportunities");
        }
    }
}
