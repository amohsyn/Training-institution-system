namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Absences",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TraineeId = c.Long(nullable: false),
                        isHaveAuthorization = c.Boolean(nullable: false),
                        SeanceTrainingId = c.Long(nullable: false),
                        FormerComment = c.String(),
                        TraineeComment = c.String(),
                        SupervisorComment = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeanceTrainings", t => t.SeanceTrainingId)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.SeanceTrainingId);
            
            CreateTable(
                "dbo.SeanceTrainings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SeanceDate = c.DateTime(nullable: false),
                        SeancePlanningId = c.Long(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeancePlannings", t => t.SeancePlanningId)
                .Index(t => t.SeancePlanningId);
            
            CreateTable(
                "dbo.SeancePlannings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingId = c.Long(nullable: false),
                        SeanceDayId = c.Long(nullable: false),
                        SeanceNumberId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeanceDays", t => t.SeanceDayId)
                .ForeignKey("dbo.SeanceNumbers", t => t.SeanceNumberId)
                .ForeignKey("dbo.Trainings", t => t.TrainingId)
                .Index(t => t.TrainingId)
                .Index(t => t.SeanceDayId)
                .Index(t => t.SeanceNumberId);
            
            CreateTable(
                "dbo.SeanceDays",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeanceNumbers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingYearId = c.Long(nullable: false),
                        ModuleTrainingId = c.Long(nullable: false),
                        FormerId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formers", t => t.FormerId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.ModuleTrainings", t => t.ModuleTrainingId)
                .ForeignKey("dbo.TrainingYears", t => t.TrainingYearId)
                .Index(t => t.TrainingYearId)
                .Index(t => t.ModuleTrainingId)
                .Index(t => t.FormerId)
                .Index(t => t.GroupId);
            
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
                        Email = c.String(nullable: false, maxLength: 65),
                        Address = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        RegistrationNumber = c.String(nullable: false, maxLength: 65),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "IX_Former_Email")
                .Index(t => t.RegistrationNumber, unique: true, name: "IX_Former_RegistrationNumber");
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingTypeId = c.Long(nullable: false),
                        TrainingYearId = c.Long(nullable: false),
                        SpecialtyId = c.Long(nullable: false),
                        YearStudyId = c.Long(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.TrainingTypes", t => t.TrainingTypeId)
                .ForeignKey("dbo.TrainingYears", t => t.TrainingYearId)
                .ForeignKey("dbo.YearStudies", t => t.YearStudyId)
                .Index(t => t.TrainingTypeId)
                .Index(t => t.TrainingYearId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.YearStudyId);
            
            CreateTable(
                "dbo.Specialties",
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
                "dbo.TrainingTypes",
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
                "dbo.TrainingYears",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndtDate = c.DateTime(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.ModuleTrainings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SpecialtyId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Cellphone = c.String(),
                        TutorCellPhone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        CNE = c.String(nullable: false, maxLength: 65),
                        isActif = c.Int(nullable: false),
                        DateRegistration = c.DateTime(),
                        NationalityId = c.Long(nullable: false),
                        SchoollevelId = c.Long(),
                        GroupId = c.Long(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FirstNameArabe = c.String(nullable: false),
                        LastNameArabe = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        BirthPlace = c.String(nullable: false),
                        Sex = c.Int(nullable: false),
                        CIN = c.String(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .ForeignKey("dbo.Schoollevels", t => t.SchoollevelId)
                .Index(t => t.CNE, unique: true, name: "IX_Trainee_CEF")
                .Index(t => t.NationalityId)
                .Index(t => t.SchoollevelId)
                .Index(t => t.GroupId);
            
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
            
            CreateTable(
                "dbo.StateOfAbseces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        TraineeId = c.Long(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId);
            
            CreateTable(
                "dbo.ActionControllerApps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        AppControllerId = c.Long(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        ControllerApp_Id = c.Long(),
                        AuthrorizationApp_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ControllerApps", t => t.ControllerApp_Id)
                .ForeignKey("dbo.AuthrorizationApps", t => t.AuthrorizationApp_Id)
                .Index(t => t.ControllerApp_Id)
                .Index(t => t.AuthrorizationApp_Id);
            
            CreateTable(
                "dbo.ControllerApps",
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
                "dbo.ApplicationParams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthrorizationApps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleAppId = c.Long(nullable: false),
                        AppControllerId = c.Long(nullable: false),
                        isAllAction = c.Boolean(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        ControllerApp_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ControllerApps", t => t.ControllerApp_Id)
                .ForeignKey("dbo.RoleApps", t => t.RoleAppId, cascadeDelete: true)
                .Index(t => t.RoleAppId)
                .Index(t => t.ControllerApp_Id);
            
            CreateTable(
                "dbo.RoleApps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassroomCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(),
                        ClassroomCategoryId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassroomCategories", t => t.ClassroomCategoryId)
                .Index(t => t.ClassroomCategoryId);
            
            CreateTable(
                "dbo.EntityPropertyShortcuts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityName = c.String(nullable: false),
                        PropertyName = c.String(nullable: false),
                        PropertyShortcutName = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogWorks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        OperationWorkType = c.Int(nullable: false),
                        OperationReference = c.String(),
                        EntityType = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classrooms", "ClassroomCategoryId", "dbo.ClassroomCategories");
            DropForeignKey("dbo.AuthrorizationApps", "RoleAppId", "dbo.RoleApps");
            DropForeignKey("dbo.AuthrorizationApps", "ControllerApp_Id", "dbo.ControllerApps");
            DropForeignKey("dbo.ActionControllerApps", "AuthrorizationApp_Id", "dbo.AuthrorizationApps");
            DropForeignKey("dbo.ActionControllerApps", "ControllerApp_Id", "dbo.ControllerApps");
            DropForeignKey("dbo.Absences", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.StateOfAbseces", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.Trainees", "SchoollevelId", "dbo.Schoollevels");
            DropForeignKey("dbo.Trainees", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.Trainees", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Absences", "SeanceTrainingId", "dbo.SeanceTrainings");
            DropForeignKey("dbo.SeanceTrainings", "SeancePlanningId", "dbo.SeancePlannings");
            DropForeignKey("dbo.SeancePlannings", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.Trainings", "TrainingYearId", "dbo.TrainingYears");
            DropForeignKey("dbo.Trainings", "ModuleTrainingId", "dbo.ModuleTrainings");
            DropForeignKey("dbo.ModuleTrainings", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.Trainings", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "YearStudyId", "dbo.YearStudies");
            DropForeignKey("dbo.Groups", "TrainingYearId", "dbo.TrainingYears");
            DropForeignKey("dbo.Groups", "TrainingTypeId", "dbo.TrainingTypes");
            DropForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.Trainings", "FormerId", "dbo.Formers");
            DropForeignKey("dbo.SeancePlannings", "SeanceNumberId", "dbo.SeanceNumbers");
            DropForeignKey("dbo.SeancePlannings", "SeanceDayId", "dbo.SeanceDays");
            DropIndex("dbo.Classrooms", new[] { "ClassroomCategoryId" });
            DropIndex("dbo.AuthrorizationApps", new[] { "ControllerApp_Id" });
            DropIndex("dbo.AuthrorizationApps", new[] { "RoleAppId" });
            DropIndex("dbo.ActionControllerApps", new[] { "AuthrorizationApp_Id" });
            DropIndex("dbo.ActionControllerApps", new[] { "ControllerApp_Id" });
            DropIndex("dbo.StateOfAbseces", new[] { "TraineeId" });
            DropIndex("dbo.Trainees", new[] { "GroupId" });
            DropIndex("dbo.Trainees", new[] { "SchoollevelId" });
            DropIndex("dbo.Trainees", new[] { "NationalityId" });
            DropIndex("dbo.Trainees", "IX_Trainee_CEF");
            DropIndex("dbo.ModuleTrainings", new[] { "SpecialtyId" });
            DropIndex("dbo.Groups", new[] { "YearStudyId" });
            DropIndex("dbo.Groups", new[] { "SpecialtyId" });
            DropIndex("dbo.Groups", new[] { "TrainingYearId" });
            DropIndex("dbo.Groups", new[] { "TrainingTypeId" });
            DropIndex("dbo.Formers", "IX_Former_RegistrationNumber");
            DropIndex("dbo.Formers", "IX_Former_Email");
            DropIndex("dbo.Trainings", new[] { "GroupId" });
            DropIndex("dbo.Trainings", new[] { "FormerId" });
            DropIndex("dbo.Trainings", new[] { "ModuleTrainingId" });
            DropIndex("dbo.Trainings", new[] { "TrainingYearId" });
            DropIndex("dbo.SeancePlannings", new[] { "SeanceNumberId" });
            DropIndex("dbo.SeancePlannings", new[] { "SeanceDayId" });
            DropIndex("dbo.SeancePlannings", new[] { "TrainingId" });
            DropIndex("dbo.SeanceTrainings", new[] { "SeancePlanningId" });
            DropIndex("dbo.Absences", new[] { "SeanceTrainingId" });
            DropIndex("dbo.Absences", new[] { "TraineeId" });
            DropTable("dbo.LogWorks");
            DropTable("dbo.EntityPropertyShortcuts");
            DropTable("dbo.Classrooms");
            DropTable("dbo.ClassroomCategories");
            DropTable("dbo.RoleApps");
            DropTable("dbo.AuthrorizationApps");
            DropTable("dbo.ApplicationParams");
            DropTable("dbo.ControllerApps");
            DropTable("dbo.ActionControllerApps");
            DropTable("dbo.StateOfAbseces");
            DropTable("dbo.Schoollevels");
            DropTable("dbo.Nationalities");
            DropTable("dbo.Trainees");
            DropTable("dbo.ModuleTrainings");
            DropTable("dbo.YearStudies");
            DropTable("dbo.TrainingYears");
            DropTable("dbo.TrainingTypes");
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
            DropTable("dbo.Formers");
            DropTable("dbo.Trainings");
            DropTable("dbo.SeanceNumbers");
            DropTable("dbo.SeanceDays");
            DropTable("dbo.SeancePlannings");
            DropTable("dbo.SeanceTrainings");
            DropTable("dbo.Absences");
        }
    }
}
