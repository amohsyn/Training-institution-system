namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trainees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
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
                        CNE = c.String(nullable: false),
                        GroupId = c.Long(nullable: false),
                        EtudiantActif = c.Boolean(nullable: false),
                        Principale = c.Boolean(nullable: false),
                        EtudiantPayant = c.Boolean(nullable: false),
                        LibelleLong = c.String(),
                        CodeDiplome = c.String(),
                        DateInscription = c.DateTime(nullable: false),
                        DateDossierComplet = c.DateTime(),
                        LieuNaissance = c.String(),
                        MotifAdmission = c.String(),
                        NTel_du_Tuteur = c.String(),
                        Nationalite = c.String(),
                        Nom_Arabe = c.String(),
                        Prenom_arabe = c.String(),
                        NiveauScolaire = c.String(),
                        AnneeEtude = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "GroupId", "dbo.Groups");
            DropIndex("dbo.Trainees", new[] { "GroupId" });
            DropTable("dbo.Trainees");
        }
    }
}
