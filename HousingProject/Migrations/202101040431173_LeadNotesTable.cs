namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeadNotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeadNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        BuyerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyer_Detail", t => t.BuyerId, cascadeDelete: true)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadNotes", "BuyerId", "dbo.Buyer_Detail");
            DropIndex("dbo.LeadNotes", new[] { "BuyerId" });
            DropTable("dbo.LeadNotes");
        }
    }
}
