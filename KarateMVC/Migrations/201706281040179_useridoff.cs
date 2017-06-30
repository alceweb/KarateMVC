namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridoff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WSKA2017", "Nome_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WSKA2017", new[] { "Nome_Id" });
            DropColumn("dbo.WSKA2017", "Nome_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WSKA2017", "Nome_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.WSKA2017", "Nome_Id");
            AddForeignKey("dbo.WSKA2017", "Nome_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
