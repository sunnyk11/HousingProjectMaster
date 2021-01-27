namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRejectedLeads : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RejectedLeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        Reason = c.String(),
                        RejectedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyer_Detail", t => t.BuyerId, cascadeDelete: true)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RejectedLeads", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.RejectedLeads", new[] { "BuyerId" });
            DropTable("dbo.RejectedLeads");
        }
    }
}
