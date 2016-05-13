namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventi1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Eventis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Descrizione = c.String(),
                        Data = c.String(),
                        Pubblica = c.Boolean(nullable: false),
                        Approfondimento = c.String(),
                        Galleria = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Eventis");
        }
    }
}
