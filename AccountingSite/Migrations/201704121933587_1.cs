namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        StatusId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Employees_Id = c.Int(),
                        From_Id = c.Int(),
                        To_Id = c.Int(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employees_Id)
                .ForeignKey("dbo.Employees", t => t.From_Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.To_Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.StatusId)
                .Index(t => t.Employees_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.ItemTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Orders", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "To_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "StatusId", "dbo.Status");
            DropForeignKey("dbo.ItemTransactions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "From_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "Employees_Id", "dbo.Employees");
            DropIndex("dbo.ItemTransactions", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Employee_Id" });
            DropIndex("dbo.Orders", new[] { "To_Id" });
            DropIndex("dbo.Orders", new[] { "From_Id" });
            DropIndex("dbo.Orders", new[] { "Employees_Id" });
            DropIndex("dbo.Orders", new[] { "StatusId" });
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropIndex("dbo.Departments", new[] { "Id" });
            DropTable("dbo.Items");
            DropTable("dbo.Roles");
            DropTable("dbo.Status");
            DropTable("dbo.ItemTransactions");
            DropTable("dbo.Orders");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
