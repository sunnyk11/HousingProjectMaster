namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerDetailForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyer_Detail", "LeadStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Buyer_Detail", "LeadStatusId");
            AddForeignKey("dbo.Buyer_Detail", "LeadStatusId", "dbo.LeadStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buyer_Detail", "LeadStatusId", "dbo.LeadStatus");
            DropIndex("dbo.Buyer_Detail", new[] { "LeadStatusId" });
            DropColumn("dbo.Buyer_Detail", "LeadStatusId");
        }
    }
}
