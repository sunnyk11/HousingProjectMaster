namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerDetailCollectionAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerContacts", "Buyer_Detail_BuyerId", c => c.Int());
            CreateIndex("dbo.CustomerContacts", "Buyer_Detail_BuyerId");
            CreateIndex("dbo.Customers", "BuyerId");
            CreateIndex("dbo.DocumentUploadeds", "BuyerId");
            CreateIndex("dbo.Opportunities", "BuyerId");
            AddForeignKey("dbo.CustomerContacts", "Buyer_Detail_BuyerId", "dbo.Buyer_Detail", "BuyerId");
            AddForeignKey("dbo.Customers", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
            AddForeignKey("dbo.DocumentUploadeds", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
            AddForeignKey("dbo.Opportunities", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opportunities", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.DocumentUploadeds", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.Customers", "BuyerId", "dbo.Buyer_Detail");
            DropForeignKey("dbo.CustomerContacts", "Buyer_Detail_BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.Opportunities", new[] { "BuyerId" });
            DropIndex("dbo.DocumentUploadeds", new[] { "BuyerId" });
            DropIndex("dbo.Customers", new[] { "BuyerId" });
            DropIndex("dbo.CustomerContacts", new[] { "Buyer_Detail_BuyerId" });
            DropColumn("dbo.CustomerContacts", "Buyer_Detail_BuyerId");
        }
    }
}
