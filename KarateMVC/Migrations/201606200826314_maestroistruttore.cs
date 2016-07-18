namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maestroistruttore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Maestro", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Istruttore", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Istruttore");
            DropColumn("dbo.AspNetUsers", "Maestro");
        }
    }
}
