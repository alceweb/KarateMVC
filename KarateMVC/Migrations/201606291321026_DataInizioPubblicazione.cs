namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataInizioPubblicazione : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventis", "DataP", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventis", "DataP");
        }
    }
}
