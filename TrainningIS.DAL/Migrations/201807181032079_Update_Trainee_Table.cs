namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Trainee_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YearStudies",
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
            
            CreateTable(
                "dbo.Nationalities",
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
            
            CreateTable(
                "dbo.Schoollevels",
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
            
            AddColumn("dbo.Groups", "YearStudyId", c => c.Long(nullable: false));
            AddColumn("dbo.Trainees", "FirstNameArabe", c => c.String(nullable: false));
            AddColumn("dbo.Trainees", "LastNameArabe", c => c.String(nullable: false));
            AddColumn("dbo.Trainees", "BirthPlace", c => c.String(nullable: false));
            AddColumn("dbo.Trainees", "TutorCellPhone", c => c.String());
            AddColumn("dbo.Trainees", "CEF", c => c.String(nullable: false));
            AddColumn("dbo.Trainees", "isActif", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trainees", "DateRegistration", c => c.DateTime());
            AddColumn("dbo.Trainees", "NationalityId", c => c.Long(nullable: false));
            AddColumn("dbo.Trainees", "SchoollevelId", c => c.Long(nullable: false));
            AlterColumn("dbo.Trainees", "CIN", c => c.String(nullable: false));
            CreateIndex("dbo.Groups", "YearStudyId");
            CreateIndex("dbo.Trainees", "NationalityId");
            CreateIndex("dbo.Trainees", "SchoollevelId");
            AddForeignKey("dbo.Groups", "YearStudyId", "dbo.YearStudies", "Id");
            AddForeignKey("dbo.Trainees", "NationalityId", "dbo.Nationalities", "Id");
            AddForeignKey("dbo.Trainees", "SchoollevelId", "dbo.Schoollevels", "Id");
            DropColumn("dbo.Groups", "Year");
            DropColumn("dbo.Trainees", "EtudiantActif");
            DropColumn("dbo.Trainees", "Principale");
            DropColumn("dbo.Trainees", "EtudiantPayant");
            DropColumn("dbo.Trainees", "LibelleLong");
            DropColumn("dbo.Trainees", "CodeDiplome");
            DropColumn("dbo.Trainees", "DateInscription");
            DropColumn("dbo.Trainees", "DateDossierComplet");
            DropColumn("dbo.Trainees", "LieuNaissance");
            DropColumn("dbo.Trainees", "MotifAdmission");
            DropColumn("dbo.Trainees", "NTel_du_Tuteur");
            DropColumn("dbo.Trainees", "Nationalite");
            DropColumn("dbo.Trainees", "Nom_Arabe");
            DropColumn("dbo.Trainees", "Prenom_arabe");
            DropColumn("dbo.Trainees", "NiveauScolaire");
            DropColumn("dbo.Trainees", "AnneeEtude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainees", "AnneeEtude", c => c.String());
            AddColumn("dbo.Trainees", "NiveauScolaire", c => c.String());
            AddColumn("dbo.Trainees", "Prenom_arabe", c => c.String());
            AddColumn("dbo.Trainees", "Nom_Arabe", c => c.String());
            AddColumn("dbo.Trainees", "Nationalite", c => c.String());
            AddColumn("dbo.Trainees", "NTel_du_Tuteur", c => c.String());
            AddColumn("dbo.Trainees", "MotifAdmission", c => c.String());
            AddColumn("dbo.Trainees", "LieuNaissance", c => c.String());
            AddColumn("dbo.Trainees", "DateDossierComplet", c => c.DateTime());
            AddColumn("dbo.Trainees", "DateInscription", c => c.DateTime());
            AddColumn("dbo.Trainees", "CodeDiplome", c => c.String());
            AddColumn("dbo.Trainees", "LibelleLong", c => c.String());
            AddColumn("dbo.Trainees", "EtudiantPayant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trainees", "Principale", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trainees", "EtudiantActif", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "Year", c => c.Int(nullable: false));
            DropForeignKey("dbo.Trainees", "SchoollevelId", "dbo.Schoollevels");
            DropForeignKey("dbo.Trainees", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.Groups", "YearStudyId", "dbo.YearStudies");
            DropIndex("dbo.Trainees", new[] { "SchoollevelId" });
            DropIndex("dbo.Trainees", new[] { "NationalityId" });
            DropIndex("dbo.Groups", new[] { "YearStudyId" });
            AlterColumn("dbo.Trainees", "CIN", c => c.String());
            DropColumn("dbo.Trainees", "SchoollevelId");
            DropColumn("dbo.Trainees", "NationalityId");
            DropColumn("dbo.Trainees", "DateRegistration");
            DropColumn("dbo.Trainees", "isActif");
            DropColumn("dbo.Trainees", "CEF");
            DropColumn("dbo.Trainees", "TutorCellPhone");
            DropColumn("dbo.Trainees", "BirthPlace");
            DropColumn("dbo.Trainees", "LastNameArabe");
            DropColumn("dbo.Trainees", "FirstNameArabe");
            DropColumn("dbo.Groups", "YearStudyId");
            DropTable("dbo.Schoollevels");
            DropTable("dbo.Nationalities");
            DropTable("dbo.YearStudies");
        }
    }
}
