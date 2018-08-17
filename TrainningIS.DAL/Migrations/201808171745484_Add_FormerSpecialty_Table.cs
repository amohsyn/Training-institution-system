namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FormerSpecialty_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Formers", "MetierId", "dbo.Metiers");
            DropIndex("dbo.Formers", new[] { "MetierId" });
            CreateTable(
                "dbo.FormerSpecialties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Formers", "FormerSpecialtyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Formers", "FormerSpecialtyId");
            AddForeignKey("dbo.Formers", "FormerSpecialtyId", "dbo.FormerSpecialties", "Id");
            DropColumn("dbo.Formers", "MetierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Formers", "MetierId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Formers", "FormerSpecialtyId", "dbo.FormerSpecialties");
            DropIndex("dbo.Formers", new[] { "FormerSpecialtyId" });
            DropColumn("dbo.Formers", "FormerSpecialtyId");
            DropTable("dbo.FormerSpecialties");
            CreateIndex("dbo.Formers", "MetierId");
            AddForeignKey("dbo.Formers", "MetierId", "dbo.Metiers", "Id");
        }
    }
}
