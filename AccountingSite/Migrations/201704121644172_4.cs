namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Surname");
            DropColumn("dbo.Employees", "Patronymic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Patronymic", c => c.String());
            AddColumn("dbo.Employees", "Surname", c => c.String());
        }
    }
}
