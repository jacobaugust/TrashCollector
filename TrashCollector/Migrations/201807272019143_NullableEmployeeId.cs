namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableEmployeeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Pickups", new[] { "EmployeeID" });
            AlterColumn("dbo.Pickups", "EmployeeID", c => c.Int());
            CreateIndex("dbo.Pickups", "EmployeeID");
            AddForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Pickups", new[] { "EmployeeID" });
            AlterColumn("dbo.Pickups", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pickups", "EmployeeID");
            AddForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
