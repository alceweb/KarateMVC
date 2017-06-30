namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WSKA2017", "Giorni", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WSKA2017", "Giorni", c => c.Int(nullable: false));
        }
    }
}
