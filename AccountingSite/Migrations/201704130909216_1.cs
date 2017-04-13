namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropIndex("dbo.Orders", new[] { "Employee_Id" });
            RenameColumn(table: "dbo.Orders", name: "EmployeeId", newName: "Employee_Id");
            AddColumn("dbo.Orders", "Employee_Id1", c => c.Int());
            AlterColumn("dbo.Orders", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Employee_Id");
            CreateIndex("dbo.Orders", "Employee_Id1");
            AddForeignKey("dbo.Orders", "Employee_Id1", "dbo.Employees", "Id");
            AddForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "Employee_Id1", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "Employee_Id1" });
            DropIndex("dbo.Orders", new[] { "Employee_Id" });
            AlterColumn("dbo.Orders", "Employee_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Employee_Id1");
            RenameColumn(table: "dbo.Orders", name: "Employee_Id", newName: "EmployeeId");
            AddColumn("dbo.Orders", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Employee_Id");
            CreateIndex("dbo.Orders", "EmployeeId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees", "Id");
        }
    }
}
