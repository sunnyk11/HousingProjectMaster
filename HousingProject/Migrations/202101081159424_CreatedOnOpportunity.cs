namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedOnOpportunity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opportunities", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opportunities", "CreatedOn");
        }
    }
}
