namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnInPropertyLister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyListers", "IsConverted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyListers", "IsConverted");
        }
    }
}
