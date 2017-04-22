namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "From_Id", "dbo.Employees");
            DropForeignKey("dbo.Orders", "To_Id", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "From_Id" });
            DropIndex("dbo.Orders", new[] { "To_Id" });
            RenameColumn(table: "dbo.Orders", name: "From_Id", newName: "FromId");
            RenameColumn(table: "dbo.Orders", name: "To_Id", newName: "ToId");
            AlterColumn("dbo.Orders", "FromId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ToId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "FromId");
            CreateIndex("dbo.Orders", "ToId");
            AddForeignKey("dbo.Orders", "FromId", "dbo.Employees", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Orders", "ToId", "dbo.Employees", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ToId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "FromId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "ToId" });
            DropIndex("dbo.Orders", new[] { "FromId" });
            AlterColumn("dbo.Orders", "ToId", c => c.Int());
            AlterColumn("dbo.Orders", "FromId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ToId", newName: "To_Id");
            RenameColumn(table: "dbo.Orders", name: "FromId", newName: "From_Id");
            CreateIndex("dbo.Orders", "To_Id");
            CreateIndex("dbo.Orders", "From_Id");
            AddForeignKey("dbo.Orders", "To_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Orders", "From_Id", "dbo.Employees", "Id");
        }
    }
}
