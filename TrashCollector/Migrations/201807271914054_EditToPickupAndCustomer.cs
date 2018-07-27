namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditToPickupAndCustomer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pickups", "ChargeAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "ChargeAmount", c => c.Double(nullable: false));
        }
    }
}
