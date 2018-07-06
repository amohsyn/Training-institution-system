namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Former : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Formers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        CIN = c.String(),
                        Cellphone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        RegistrationNumber = c.String(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Formers");
        }
    }
}
