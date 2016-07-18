namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventis", "DataF", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventis", "DataF");
        }
    }
}
