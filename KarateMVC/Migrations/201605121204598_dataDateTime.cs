namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Eventis", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Eventis", "Data", c => c.String());
        }
    }
}
