namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WSKA2017", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.WSKA2017", new[] { "Id" });
            AddColumn("dbo.WSKA2017", "UId", c => c.String());
            DropColumn("dbo.WSKA2017", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WSKA2017", "Id", c => c.String(maxLength: 128));
            DropColumn("dbo.WSKA2017", "UId");
            CreateIndex("dbo.WSKA2017", "Id");
            AddForeignKey("dbo.WSKA2017", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
