namespace TrashCollector
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Employee_Id", c => c.Int());
            AddColumn("dbo.Employees", "Name", c => c.String());
            AddColumn("dbo.Employees", "employeeZipCode", c => c.String());
            AddColumn("dbo.Pickups", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.Pickups", "CustomerID", c => c.Int(nullable: false));
            AddColumn("dbo.Pickups", "pickUpDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Customers", "Employee_Id");
            CreateIndex("dbo.Pickups", "EmployeeID");
            CreateIndex("dbo.Pickups", "CustomerID");
            AddForeignKey("dbo.Customers", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Pickups", "CustomerID", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees", "Id", cascadeDelete: true);
            DropColumn("dbo.Employees", "TrashPickupsToday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "TrashPickupsToday", c => c.String());
            DropForeignKey("dbo.Pickups", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Pickups", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Pickups", new[] { "CustomerID" });
            DropIndex("dbo.Pickups", new[] { "EmployeeID" });
            DropIndex("dbo.Customers", new[] { "Employee_Id" });
            DropColumn("dbo.Pickups", "pickUpDate");
            DropColumn("dbo.Pickups", "CustomerID");
            DropColumn("dbo.Pickups", "EmployeeID");
            DropColumn("dbo.Employees", "employeeZipCode");
            DropColumn("dbo.Employees", "Name");
            DropColumn("dbo.Customers", "Employee_Id");
        }
    }
}
