namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusColumnInOpportunity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OpportunityStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Opportunities", "OpportunityStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Opportunities", "OppoStatus_Id", c => c.Int());
            CreateIndex("dbo.Opportunities", "OppoStatus_Id");
            AddForeignKey("dbo.Opportunities", "OppoStatus_Id", "dbo.OpportunityStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opportunities", "OppoStatus_Id", "dbo.OpportunityStatus");
            DropIndex("dbo.Opportunities", new[] { "OppoStatus_Id" });
            DropColumn("dbo.Opportunities", "OppoStatus_Id");
            DropColumn("dbo.Opportunities", "OpportunityStatus");
            DropTable("dbo.OpportunityStatus");
        }
    }
}
