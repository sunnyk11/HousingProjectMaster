namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyInOpportunityUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Opportunities", "OpportunityStatus");
            RenameColumn(table: "dbo.Opportunities", name: "OppoStatus_Id", newName: "OpportunityStatus");
            RenameIndex(table: "dbo.Opportunities", name: "IX_OppoStatus_Id", newName: "IX_OpportunityStatus");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Opportunities", name: "IX_OpportunityStatus", newName: "IX_OppoStatus_Id");
            RenameColumn(table: "dbo.Opportunities", name: "OpportunityStatus", newName: "OppoStatus_Id");
            AddColumn("dbo.Opportunities", "OpportunityStatus", c => c.Int());
        }
    }
}
