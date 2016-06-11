namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Kata", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Kata");
        }
    }
}
