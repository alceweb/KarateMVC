namespace KarateMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Medagliere : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medaglieres",
                c => new
                    {
                        Medagliere_Id = c.Int(nullable: false, identity: true),
                        Evento_Id = c.Int(nullable: false),
                        Spec = c.String(),
                        classifica = c.String(),
                    })
                .PrimaryKey(t => t.Medagliere_Id)
                .ForeignKey("dbo.Eventis", t => t.Evento_Id, cascadeDelete: true)
                .Index(t => t.Evento_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medaglieres", "Evento_Id", "dbo.Eventis");
            DropIndex("dbo.Medaglieres", new[] { "Evento_Id" });
            DropTable("dbo.Medaglieres");
        }
    }
}
