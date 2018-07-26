namespace TrashCollector
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "EmployeeID");
            AddForeignKey("dbo.AspNetUsers", "EmployeeID", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.AspNetUsers", new[] { "EmployeeID" });
            DropColumn("dbo.AspNetUsers", "EmployeeID");
        }
    }
}
