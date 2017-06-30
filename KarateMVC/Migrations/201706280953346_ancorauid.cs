namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ancorauid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WSKA2017", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.WSKA2017", "Id");
            AddForeignKey("dbo.WSKA2017", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WSKA2017", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.WSKA2017", new[] { "Id" });
            AlterColumn("dbo.WSKA2017", "Id", c => c.String());
        }
    }
}
