namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomecognome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WSKA2017", "Nome", c => c.String());
            AddColumn("dbo.WSKA2017", "Cognome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WSKA2017", "Cognome");
            DropColumn("dbo.WSKA2017", "Nome");
        }
    }
}
