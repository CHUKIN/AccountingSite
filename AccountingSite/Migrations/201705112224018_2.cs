namespace AccountingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Login", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Employees", "Password", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Password", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "Login", c => c.String(unicode: false));
        }
    }
}
