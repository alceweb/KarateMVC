namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostoInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WSKA2017", "Costo", c => c.Int(nullable: false));
            AlterColumn("dbo.WSKA2017", "Totale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WSKA2017", "Totale", c => c.Single());
            AlterColumn("dbo.WSKA2017", "Costo", c => c.Single());
        }
    }
}
