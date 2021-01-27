namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsconvertedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Buyer_Detail", "IsConverted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Buyer_Detail", "IsConverted", c => c.Boolean(nullable: false));
        }
    }
}
