namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WSKA2017", name: "Id", newName: "Nome_Id");
            RenameIndex(table: "dbo.WSKA2017", name: "IX_Id", newName: "IX_Nome_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.WSKA2017", name: "IX_Nome_Id", newName: "IX_Id");
            RenameColumn(table: "dbo.WSKA2017", name: "Nome_Id", newName: "Id");
        }
    }
}
