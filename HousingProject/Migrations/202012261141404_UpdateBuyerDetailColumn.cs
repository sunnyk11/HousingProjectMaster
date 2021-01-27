namespace HousingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBuyerDetailColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyer_Detail", "IsAlreadyCustomer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buyer_Detail", "IsAlreadyCustomer");
        }
    }
}
