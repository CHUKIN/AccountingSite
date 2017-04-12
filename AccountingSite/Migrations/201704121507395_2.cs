namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            AddColumn("dbo.Orders", "Employees_Id", c => c.Int());
            AddColumn("dbo.Orders", "From_Id", c => c.Int());
            AddColumn("dbo.Orders", "To_Id", c => c.Int());
            AddColumn("dbo.Orders", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Employees_Id");
            CreateIndex("dbo.Orders", "From_Id");
            CreateIndex("dbo.Orders", "To_Id");
            CreateIndex("dbo.Orders", "Employee_Id");
            AddForeignKey("dbo.Orders", "From_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Orders", "To_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Orders", "Employees_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Orders", "From");
            DropColumn("dbo.Orders", "To");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "To", c => c.String());
            AddColumn("dbo.Orders", "From", c => c.String());
            DropForeignKey("dbo.Orders", "Employees_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "To_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "From_Id", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "Employee_Id" });
            DropIndex("dbo.Orders", new[] { "To_Id" });
            DropIndex("dbo.Orders", new[] { "From_Id" });
            DropIndex("dbo.Orders", new[] { "Employees_Id" });
            DropColumn("dbo.Orders", "Employee_Id");
            DropColumn("dbo.Orders", "To_Id");
            DropColumn("dbo.Orders", "From_Id");
            DropColumn("dbo.Orders", "Employees_Id");
            CreateIndex("dbo.Orders", "EmployeeId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
