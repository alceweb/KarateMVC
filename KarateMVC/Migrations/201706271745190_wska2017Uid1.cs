namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wska2017Uid1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSKA2017", "Numero", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSKA2017", "Numero");
        }
    }
}
