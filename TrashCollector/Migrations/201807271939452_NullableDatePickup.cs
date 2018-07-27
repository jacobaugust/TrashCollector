namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDatePickup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pickups", "pickUpDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pickups", "pickUpDate", c => c.DateTime(nullable: false));
        }
    }
}
