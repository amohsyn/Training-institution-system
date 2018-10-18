namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sanctions_Classes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Formers", "IX_Former_RegistrationNumber");
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MeetingDate = c.DateTime(nullable: false),
                        WorkGroupId = c.Long(nullable: false),
                        Mission_Working_GroupId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mission_Working_Group", t => t.Mission_Working_GroupId)
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroupId)
                .Index(t => t.WorkGroupId)
                .Index(t => t.Mission_Working_GroupId);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        CreateUserAccount = c.Boolean(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FirstNameArabe = c.String(),
                        LastNameArabe = c.String(),
                        Sex = c.Int(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        NationalityId = c.Long(nullable: false),
                        BirthPlace = c.String(),
                        CIN = c.String(),
                        Cellphone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Photo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .ForeignKey("dbo.GPictures", t => t.Photo_Id)
                .Index(t => t.NationalityId)
                .Index(t => t.Photo_Id);
            
            CreateTable(
                "dbo.WorkGroups",
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
                "dbo.Mission_Working_Group",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        DecisionAuthority = c.Int(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sanctions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SanctionCategoryId = c.Long(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Meeting_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id)
                .ForeignKey("dbo.SanctionCategories", t => t.SanctionCategoryId)
                .Index(t => t.SanctionCategoryId)
                .Index(t => t.Meeting_Id);
            
            CreateTable(
                "dbo.SanctionCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        DecisionAuthority = c.Int(nullable: false),
                        WorkflowOrder = c.Int(nullable: false),
                        Number_Of_Days_Of_Exclusion = c.Int(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Functions",
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
                "dbo.AdministratorMeetings",
                c => new
                    {
                        Administrator_Id = c.Long(nullable: false),
                        Meeting_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Administrator_Id, t.Meeting_Id })
                .ForeignKey("dbo.Administrators", t => t.Administrator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .Index(t => t.Administrator_Id)
                .Index(t => t.Meeting_Id);
            
            CreateTable(
                "dbo.WorkGroupAdministrators",
                c => new
                    {
                        WorkGroup_Id = c.Long(nullable: false),
                        Administrator_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkGroup_Id, t.Administrator_Id })
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Administrators", t => t.Administrator_Id, cascadeDelete: true)
                .Index(t => t.WorkGroup_Id)
                .Index(t => t.Administrator_Id);
            
            CreateTable(
                "dbo.FormerMeetings",
                c => new
                    {
                        Former_Id = c.Long(nullable: false),
                        Meeting_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Former_Id, t.Meeting_Id })
                .ForeignKey("dbo.Formers", t => t.Former_Id, cascadeDelete: true)
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .Index(t => t.Former_Id)
                .Index(t => t.Meeting_Id);
            
            CreateTable(
                "dbo.FormerWorkGroups",
                c => new
                    {
                        Former_Id = c.Long(nullable: false),
                        WorkGroup_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Former_Id, t.WorkGroup_Id })
                .ForeignKey("dbo.Formers", t => t.Former_Id, cascadeDelete: true)
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroup_Id, cascadeDelete: true)
                .Index(t => t.Former_Id)
                .Index(t => t.WorkGroup_Id);
            
            CreateTable(
                "dbo.Mission_Working_GroupWorkGroup",
                c => new
                    {
                        Mission_Working_Group_Id = c.Long(nullable: false),
                        WorkGroup_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Mission_Working_Group_Id, t.WorkGroup_Id })
                .ForeignKey("dbo.Mission_Working_Group", t => t.Mission_Working_Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroup_Id, cascadeDelete: true)
                .Index(t => t.Mission_Working_Group_Id)
                .Index(t => t.WorkGroup_Id);
            
            CreateTable(
                "dbo.WorkGroupTrainees",
                c => new
                    {
                        WorkGroup_Id = c.Long(nullable: false),
                        Trainee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkGroup_Id, t.Trainee_Id })
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.Trainee_Id, cascadeDelete: true)
                .Index(t => t.WorkGroup_Id)
                .Index(t => t.Trainee_Id);
            
            CreateTable(
                "dbo.MeetingTrainees",
                c => new
                    {
                        Meeting_Id = c.Long(nullable: false),
                        Trainee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Trainee_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.Trainee_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Trainee_Id);
            
            CreateTable(
                "dbo.SanctionAbsences",
                c => new
                    {
                        Sanction_Id = c.Long(nullable: false),
                        Absence_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sanction_Id, t.Absence_Id })
                .ForeignKey("dbo.Sanctions", t => t.Sanction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Absences", t => t.Absence_Id, cascadeDelete: true)
                .Index(t => t.Sanction_Id)
                .Index(t => t.Absence_Id);
            
            AlterColumn("dbo.Formers", "RegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sanctions", "SanctionCategoryId", "dbo.SanctionCategories");
            DropForeignKey("dbo.Sanctions", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.SanctionAbsences", "Absence_Id", "dbo.Absences");
            DropForeignKey("dbo.SanctionAbsences", "Sanction_Id", "dbo.Sanctions");
            DropForeignKey("dbo.Meetings", "WorkGroupId", "dbo.WorkGroups");
            DropForeignKey("dbo.MeetingTrainees", "Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.MeetingTrainees", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Meetings", "Mission_Working_GroupId", "dbo.Mission_Working_Group");
            DropForeignKey("dbo.WorkGroupTrainees", "Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.WorkGroupTrainees", "WorkGroup_Id", "dbo.WorkGroups");
            DropForeignKey("dbo.Mission_Working_GroupWorkGroup", "WorkGroup_Id", "dbo.WorkGroups");
            DropForeignKey("dbo.Mission_Working_GroupWorkGroup", "Mission_Working_Group_Id", "dbo.Mission_Working_Group");
            DropForeignKey("dbo.FormerWorkGroups", "WorkGroup_Id", "dbo.WorkGroups");
            DropForeignKey("dbo.FormerWorkGroups", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.FormerMeetings", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.FormerMeetings", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroupAdministrators", "Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.WorkGroupAdministrators", "WorkGroup_Id", "dbo.WorkGroups");
            DropForeignKey("dbo.Administrators", "Photo_Id", "dbo.GPictures");
            DropForeignKey("dbo.Administrators", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.AdministratorMeetings", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.AdministratorMeetings", "Administrator_Id", "dbo.Administrators");
            DropIndex("dbo.SanctionAbsences", new[] { "Absence_Id" });
            DropIndex("dbo.SanctionAbsences", new[] { "Sanction_Id" });
            DropIndex("dbo.MeetingTrainees", new[] { "Trainee_Id" });
            DropIndex("dbo.MeetingTrainees", new[] { "Meeting_Id" });
            DropIndex("dbo.WorkGroupTrainees", new[] { "Trainee_Id" });
            DropIndex("dbo.WorkGroupTrainees", new[] { "WorkGroup_Id" });
            DropIndex("dbo.Mission_Working_GroupWorkGroup", new[] { "WorkGroup_Id" });
            DropIndex("dbo.Mission_Working_GroupWorkGroup", new[] { "Mission_Working_Group_Id" });
            DropIndex("dbo.FormerWorkGroups", new[] { "WorkGroup_Id" });
            DropIndex("dbo.FormerWorkGroups", new[] { "Former_Id" });
            DropIndex("dbo.FormerMeetings", new[] { "Meeting_Id" });
            DropIndex("dbo.FormerMeetings", new[] { "Former_Id" });
            DropIndex("dbo.WorkGroupAdministrators", new[] { "Administrator_Id" });
            DropIndex("dbo.WorkGroupAdministrators", new[] { "WorkGroup_Id" });
            DropIndex("dbo.AdministratorMeetings", new[] { "Meeting_Id" });
            DropIndex("dbo.AdministratorMeetings", new[] { "Administrator_Id" });
            DropIndex("dbo.Sanctions", new[] { "Meeting_Id" });
            DropIndex("dbo.Sanctions", new[] { "SanctionCategoryId" });
            DropIndex("dbo.Administrators", new[] { "Photo_Id" });
            DropIndex("dbo.Administrators", new[] { "NationalityId" });
            DropIndex("dbo.Meetings", new[] { "Mission_Working_GroupId" });
            DropIndex("dbo.Meetings", new[] { "WorkGroupId" });
            AlterColumn("dbo.Formers", "RegistrationNumber", c => c.String(nullable: false, maxLength: 65));
            DropTable("dbo.SanctionAbsences");
            DropTable("dbo.MeetingTrainees");
            DropTable("dbo.WorkGroupTrainees");
            DropTable("dbo.Mission_Working_GroupWorkGroup");
            DropTable("dbo.FormerWorkGroups");
            DropTable("dbo.FormerMeetings");
            DropTable("dbo.WorkGroupAdministrators");
            DropTable("dbo.AdministratorMeetings");
            DropTable("dbo.Functions");
            DropTable("dbo.SanctionCategories");
            DropTable("dbo.Sanctions");
            DropTable("dbo.Mission_Working_Group");
            DropTable("dbo.WorkGroups");
            DropTable("dbo.Administrators");
            DropTable("dbo.Meetings");
            CreateIndex("dbo.Formers", "RegistrationNumber", unique: true, name: "IX_Former_RegistrationNumber");
        }
    }
}
