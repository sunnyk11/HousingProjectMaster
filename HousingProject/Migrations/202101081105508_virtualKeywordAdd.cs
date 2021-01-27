namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtualKeywordAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.LeadNotes", new[] { "BuyerId" });
            AlterColumn("dbo.LeadNotes", "BuyerId", c => c.Int(nullable: false));
            CreateIndex("dbo.LeadNotes", "BuyerId");
            AddForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.LeadNotes", new[] { "BuyerId" });
            AlterColumn("dbo.LeadNotes", "BuyerId", c => c.Int());
            CreateIndex("dbo.LeadNotes", "BuyerId");
            AddForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail", "BuyerId");
        }
    }
}
