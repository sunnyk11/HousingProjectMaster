namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerDetailCollectionRemove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerContacts", "Buyer_Detail_BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.CustomerContacts", new[] { "Buyer_Detail_BuyerId" });
            DropIndex("dbo.LeadNotes", new[] { "BuyerId" });
            AlterColumn("dbo.LeadNotes", "BuyerId", c => c.Int());
            CreateIndex("dbo.LeadNotes", "BuyerId");
            AddForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail", "BuyerId");
            DropColumn("dbo.CustomerContacts", "Buyer_Detail_BuyerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerContacts", "Buyer_Detail_BuyerId", c => c.Int());
            DropForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.LeadNotes", new[] { "BuyerId" });
            AlterColumn("dbo.LeadNotes", "BuyerId", c => c.Int(nullable: false));
            CreateIndex("dbo.LeadNotes", "BuyerId");
            CreateIndex("dbo.CustomerContacts", "Buyer_Detail_BuyerId");
            AddForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
            AddForeignKey("dbo.CustomerContacts", "Buyer_Detail_BuyerId", "dbo.Buyer_Detail", "BuyerId");
        }
    }
}
