namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datadescrizionerenew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventis", "Descrizione", c => c.String());
            AddColumn("dbo.Eventis", "Data", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventis", "Data");
            DropColumn("dbo.Eventis", "Descrizione");
        }
    }
}
