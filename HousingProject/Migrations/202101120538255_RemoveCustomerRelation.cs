namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomerRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerContacts", new[] { "CustomerId" });
            RenameColumn(table: "dbo.CustomerContacts", name: "CustomerId", newName: "Customer_CustomerId");
            AddColumn("dbo.CustomerContacts", "BuyerId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerContacts", "ContactMobile", c => c.String());
            AddColumn("dbo.CustomerContacts", "ContactName", c => c.String());
            AlterColumn("dbo.CustomerContacts", "Customer_CustomerId", c => c.Int());
            CreateIndex("dbo.CustomerContacts", "BuyerId");
            CreateIndex("dbo.CustomerContacts", "Customer_CustomerId");
            AddForeignKey("dbo.CustomerContacts", "BuyerId", "dbo.Buyer_Detail", "BuyerId", cascadeDelete: true);
            AddForeignKey("dbo.CustomerContacts", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            DropColumn("dbo.CustomerContacts", "Contact1Mobile");
            DropColumn("dbo.CustomerContacts", "Contact1Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerContacts", "Contact1Name", c => c.String());
            AddColumn("dbo.CustomerContacts", "Contact1Mobile", c => c.String());
            DropForeignKey("dbo.CustomerContacts", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerContacts", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.CustomerContacts", new[] { "Customer_CustomerId" });
            DropIndex("dbo.CustomerContacts", new[] { "BuyerId" });
            AlterColumn("dbo.CustomerContacts", "Customer_CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerContacts", "ContactName");
            DropColumn("dbo.CustomerContacts", "ContactMobile");
            DropColumn("dbo.CustomerContacts", "BuyerId");
            RenameColumn(table: "dbo.CustomerContacts", name: "Customer_CustomerId", newName: "CustomerId");
            CreateIndex("dbo.CustomerContacts", "CustomerId");
            AddForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
