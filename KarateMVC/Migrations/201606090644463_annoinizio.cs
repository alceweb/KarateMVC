namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class annoinizio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AnnoInizio", c => c.String());
            DropColumn("dbo.AspNetUsers", "DataInizio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DataInizio", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "AnnoInizio");
        }
    }
}
