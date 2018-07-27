namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SpecialPickupDay", c => c.DateTime());
            AddColumn("dbo.Customers", "SuspendPickupStartDate", c => c.DateTime());
            AddColumn("dbo.Customers", "SuspendPickupEndDate", c => c.DateTime());
            AddColumn("dbo.Customers", "MonthlyBalance", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "MonthlyBalance");
            DropColumn("dbo.Customers", "SuspendPickupEndDate");
            DropColumn("dbo.Customers", "SuspendPickupStartDate");
            DropColumn("dbo.Customers", "SpecialPickupDay");
        }
    }
}
