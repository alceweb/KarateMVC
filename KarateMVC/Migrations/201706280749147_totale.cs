namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSKA2017", "Totale", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSKA2017", "Totale");
        }
    }
}
