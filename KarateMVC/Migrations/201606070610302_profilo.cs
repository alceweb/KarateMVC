namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profilo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataNascita", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DataInizio", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Grado", c => c.String());
            AddColumn("dbo.AspNetUsers", "Frase", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Frase");
            DropColumn("dbo.AspNetUsers", "Grado");
            DropColumn("dbo.AspNetUsers", "DataInizio");
            DropColumn("dbo.AspNetUsers", "DataNascita");
        }
    }
}
