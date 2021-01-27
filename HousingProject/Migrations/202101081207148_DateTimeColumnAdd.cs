namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeColumnAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyer_Detail", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Buyer_Detail", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Customers", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Opportunities", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.CustomerContacts", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerContacts", "ModifiedOn", c => c.DateTime());
            DropColumn("dbo.CustomerContacts", "Contact2Mobile");
            DropColumn("dbo.CustomerContacts", "Contact2Name");
            DropColumn("dbo.CustomerContacts", "Contact3Mobile");
            DropColumn("dbo.CustomerContacts", "Contact3Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerContacts", "Contact3Name", c => c.String());
            AddColumn("dbo.CustomerContacts", "Contact3Mobile", c => c.String());
            AddColumn("dbo.CustomerContacts", "Contact2Name", c => c.String());
            AddColumn("dbo.CustomerContacts", "Contact2Mobile", c => c.String());
            DropColumn("dbo.CustomerContacts", "ModifiedOn");
            DropColumn("dbo.CustomerContacts", "CreatedOn");
            DropColumn("dbo.Opportunities", "ModifiedOn");
            DropColumn("dbo.Customers", "ModifiedOn");
            DropColumn("dbo.Customers", "CreatedOn");
            DropColumn("dbo.Buyer_Detail", "ModifiedOn");
            DropColumn("dbo.Buyer_Detail", "CreatedOn");
        }
    }
}
