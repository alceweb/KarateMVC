namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GareFikta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gares", "Fikta", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gares", "Fikta");
        }
    }
}
