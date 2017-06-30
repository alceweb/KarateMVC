namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wska2017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WSKA2017",
                c => new
                    {
                        Wska2017_id = c.Int(nullable: false, identity: true),
                        DataP = c.DateTime(nullable: false),
                        Giorni = c.Int(nullable: false),
                        Costo = c.Single(),
                        Pagato = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Wska2017_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WSKA2017");
        }
    }
}
