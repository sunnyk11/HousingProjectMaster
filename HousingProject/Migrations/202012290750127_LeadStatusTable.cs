namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeadStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeadStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Buyer_Detail", "LeadPosition");
            DropColumn("dbo.Buyer_Detail", "TransferRequestStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buyer_Detail", "TransferRequestStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Buyer_Detail", "LeadPosition", c => c.Int(nullable: false));
            DropTable("dbo.LeadStatus");
        }
    }
}
