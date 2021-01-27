namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertylisterTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListerDocumentsUploadeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListerId = c.Int(nullable: false),
                        FileName = c.String(),
                        UploadedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyListers", t => t.ListerId, cascadeDelete: true)
                .Index(t => t.ListerId);
            
            CreateTable(
                "dbo.PropertyListers",
                c => new
                    {
                        ListerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        MobileNo = c.String(),
                        ListerStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ListerId)
                .ForeignKey("dbo.ListerStatus", t => t.ListerStatus, cascadeDelete: true)
                .Index(t => t.ListerStatus);
            
            CreateTable(
                "dbo.ListerNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                        ListerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyListers", t => t.ListerId, cascadeDelete: true)
                .Index(t => t.ListerId);
            
            CreateTable(
                "dbo.ListerStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListerDocumentsUploadeds", "ListerId", "dbo.PropertyListers");
            DropForeignKey("dbo.PropertyListers", "ListerStatus", "dbo.ListerStatus");
            DropForeignKey("dbo.ListerNotes", "ListerId", "dbo.PropertyListers");
            DropIndex("dbo.ListerNotes", new[] { "ListerId" });
            DropIndex("dbo.PropertyListers", new[] { "ListerStatus" });
            DropIndex("dbo.ListerDocumentsUploadeds", new[] { "ListerId" });
            DropTable("dbo.ListerStatus");
            DropTable("dbo.ListerNotes");
            DropTable("dbo.PropertyListers");
            DropTable("dbo.ListerDocumentsUploadeds");
        }
    }
}
