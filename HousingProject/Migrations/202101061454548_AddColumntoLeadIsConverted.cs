namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumntoLeadIsConverted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyer_Detail", "IsConverted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buyer_Detail", "IsConverted");
        }
    }
}
