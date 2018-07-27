namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickupDate", c => c.DateTime());
            DropColumn("dbo.Customers", "ScheduledPickUpDay");
            DropColumn("dbo.Customers", "SpecialPickUpDay");
            DropColumn("dbo.Customers", "SuspendPickupStartDate");
            DropColumn("dbo.Customers", "SuspendPickupEndDate");
            DropColumn("dbo.Customers", "MonthlyBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MonthlyBalance", c => c.String());
            AddColumn("dbo.Customers", "SuspendPickupEndDate", c => c.String());
            AddColumn("dbo.Customers", "SuspendPickupStartDate", c => c.String());
            AddColumn("dbo.Customers", "SpecialPickUpDay", c => c.String());
            AddColumn("dbo.Customers", "ScheduledPickUpDay", c => c.String());
            DropColumn("dbo.Customers", "PickupDate");
        }
    }
}
