namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class squadre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Squadres",
                c => new
                    {
                        Squadra_id = c.Int(nullable: false, identity: true),
                        Anno = c.Int(nullable: false),
                        NomeSquadra = c.String(),
                    })
                .PrimaryKey(t => t.Squadra_id);
            
            CreateTable(
                "dbo.SquadraDetts",
                c => new
                    {
                        SquadraDett_id = c.Int(nullable: false, identity: true),
                        Squadra_id = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SquadraDett_id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Squadres", t => t.Squadra_id, cascadeDelete: true)
                .Index(t => t.Squadra_id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SquadraDetts", "Squadra_id", "dbo.Squadres");
            DropForeignKey("dbo.SquadraDetts", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.SquadraDetts", new[] { "Id" });
            DropIndex("dbo.SquadraDetts", new[] { "Squadra_id" });
            DropTable("dbo.SquadraDetts");
            DropTable("dbo.Squadres");
        }
    }
}
