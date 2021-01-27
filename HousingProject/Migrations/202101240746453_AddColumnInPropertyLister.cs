namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnInPropertyLister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyListers", "DoB", c => c.String());
            AddColumn("dbo.PropertyListers", "FatherFirstName", c => c.String());
            AddColumn("dbo.PropertyListers", "FatherLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyListers", "FatherLastName");
            DropColumn("dbo.PropertyListers", "FatherFirstName");
            DropColumn("dbo.PropertyListers", "DoB");
        }
    }
}
