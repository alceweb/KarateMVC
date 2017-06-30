namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wska2017Uid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSKA2017", "Uid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSKA2017", "Uid");
        }
    }
}
