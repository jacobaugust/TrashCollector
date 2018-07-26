namespace TrashCollector
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelsInitial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduledPickUpDay = c.String(),
                        SpecialPickUpDay = c.String(),
                        SuspendPickupStartDate = c.String(),
                        SuspendPickupEndDate = c.String(),
                        MonthlyBalance = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrashPickupsToday = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChargeAmount = c.Double(nullable: false),
                        pickupCompleted = c.Boolean(nullable: false),
                        pickupStreetAddress = c.String(),
                        pickupCity = c.String(),
                        pickupState = c.String(),
                        pickupZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pickups");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
