namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datdescrizionedel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Eventis", "Descrizione");
            DropColumn("dbo.Eventis", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Eventis", "Data", c => c.String());
            AddColumn("dbo.Eventis", "Descrizione", c => c.String());
        }
    }
}
