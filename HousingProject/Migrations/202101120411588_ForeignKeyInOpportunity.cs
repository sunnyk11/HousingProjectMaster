namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyInOpportunity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Opportunities", "OpportunityStatus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Opportunities", "OpportunityStatus", c => c.Int(nullable: false));
        }
    }
}
