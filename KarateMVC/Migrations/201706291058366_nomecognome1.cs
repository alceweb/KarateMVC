namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomecognome1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WSKA2017", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.WSKA2017", "Cognome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WSKA2017", "Cognome", c => c.String());
            AlterColumn("dbo.WSKA2017", "Nome", c => c.String());
        }
    }
}
