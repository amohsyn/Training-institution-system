namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Members_Columns_to_meetings : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WorkGroupAdministrators", newName: "WorkGroups_Memebers_Administrators");
            RenameTable(name: "dbo.WorkGroupTrainees", newName: "WorkGroups_Memebers_Trainees");
            RenameTable(name: "dbo.MeetingTrainees", newName: "Meetings_Presences_Of_Guests_Trainees");
            DropForeignKey("dbo.AdministratorMeetings", "Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.AdministratorMeetings", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.FormerMeetings", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.FormerMeetings", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.FormerWorkGroups", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.FormerWorkGroups", "WorkGroup_Id", "dbo.WorkGroups");
            DropIndex("dbo.AdministratorMeetings", new[] { "Administrator_Id" });
            DropIndex("dbo.AdministratorMeetings", new[] { "Meeting_Id" });
            DropIndex("dbo.FormerMeetings", new[] { "Former_Id" });
            DropIndex("dbo.FormerMeetings", new[] { "Meeting_Id" });
            DropIndex("dbo.FormerWorkGroups", new[] { "Former_Id" });
            DropIndex("dbo.FormerWorkGroups", new[] { "WorkGroup_Id" });
            CreateTable(
                "dbo.WorkGroups_Memebers_Formers",
                c => new
                    {
                        WorkGroup_Id = c.Long(nullable: false),
                        Former_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkGroup_Id, t.Former_Id })
                .ForeignKey("dbo.WorkGroups", t => t.WorkGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Formers", t => t.Former_Id, cascadeDelete: true)
                .Index(t => t.WorkGroup_Id)
                .Index(t => t.Former_Id);
            
            CreateTable(
                "dbo.Meetings_Presences_Of_Administrators",
                c => new
                    {
                        Meeting_Id = c.Long(nullable: false),
                        Administrator_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Administrator_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Administrators", t => t.Administrator_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Administrator_Id);
            
            CreateTable(
                "dbo.Meetings_Presences_Of_Formers",
                c => new
                    {
                        Meeting_Id = c.Long(nullable: false),
                        Former_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Former_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Formers", t => t.Former_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Former_Id);
            
            CreateTable(
                "dbo.Meetings_Presences_Of_Guests_Administrators",
                c => new
                    {
                        Meeting_Id = c.Long(nullable: false),
                        Administrator_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Administrator_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Administrators", t => t.Administrator_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Administrator_Id);
            
            CreateTable(
                "dbo.Meetings_Presences_Of_Guests_Formers",
                c => new
                    {
                        Meeting_Id = c.Long(nullable: false),
                        Former_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Former_Id })
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Formers", t => t.Former_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Former_Id);
            
            CreateTable(
                "dbo.Meetings_Presences_Of_Trainees",
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
            
            AddColumn("dbo.Meetings", "Presence_Of_President", c => c.Boolean(nullable: false));
            AddColumn("dbo.Meetings", "Presence_Of_VicePresident", c => c.Boolean(nullable: false));
            AddColumn("dbo.Meetings", "Presence_Of_Protractor", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkGroups", "GuestFormers", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkGroups", "GuestTrainees", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkGroups", "GuestAdministrator", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkGroups", "Administrator_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "Former_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "President_Administrator_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "President_Former_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "President_Trainee_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "Protractor_Administrator_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "Protractor_Former_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "Protractor_Trainee_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "VicePresident_Administrator_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "VicePresident_Former_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "VicePresident_Trainee_Id", c => c.Long());
            AddColumn("dbo.WorkGroups", "Trainee_Id", c => c.Long());
            CreateIndex("dbo.WorkGroups", "Administrator_Id");
            CreateIndex("dbo.WorkGroups", "Former_Id");
            CreateIndex("dbo.WorkGroups", "President_Administrator_Id");
            CreateIndex("dbo.WorkGroups", "President_Former_Id");
            CreateIndex("dbo.WorkGroups", "President_Trainee_Id");
            CreateIndex("dbo.WorkGroups", "Protractor_Administrator_Id");
            CreateIndex("dbo.WorkGroups", "Protractor_Former_Id");
            CreateIndex("dbo.WorkGroups", "Protractor_Trainee_Id");
            CreateIndex("dbo.WorkGroups", "VicePresident_Administrator_Id");
            CreateIndex("dbo.WorkGroups", "VicePresident_Former_Id");
            CreateIndex("dbo.WorkGroups", "VicePresident_Trainee_Id");
            CreateIndex("dbo.WorkGroups", "Trainee_Id");
            AddForeignKey("dbo.WorkGroups", "Administrator_Id", "dbo.Administrators", "Id");
            AddForeignKey("dbo.WorkGroups", "Former_Id", "dbo.Formers", "Id");
            AddForeignKey("dbo.WorkGroups", "President_Administrator_Id", "dbo.Administrators", "Id");
            AddForeignKey("dbo.WorkGroups", "President_Former_Id", "dbo.Formers", "Id");
            AddForeignKey("dbo.WorkGroups", "President_Trainee_Id", "dbo.Trainees", "Id");
            AddForeignKey("dbo.WorkGroups", "Protractor_Administrator_Id", "dbo.Administrators", "Id");
            AddForeignKey("dbo.WorkGroups", "Protractor_Former_Id", "dbo.Formers", "Id");
            AddForeignKey("dbo.WorkGroups", "Protractor_Trainee_Id", "dbo.Trainees", "Id");
            AddForeignKey("dbo.WorkGroups", "VicePresident_Administrator_Id", "dbo.Administrators", "Id");
            AddForeignKey("dbo.WorkGroups", "VicePresident_Former_Id", "dbo.Formers", "Id");
            AddForeignKey("dbo.WorkGroups", "VicePresident_Trainee_Id", "dbo.Trainees", "Id");
            AddForeignKey("dbo.WorkGroups", "Trainee_Id", "dbo.Trainees", "Id");
            DropTable("dbo.AdministratorMeetings");
            DropTable("dbo.FormerMeetings");
            DropTable("dbo.FormerWorkGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FormerWorkGroups",
                c => new
                    {
                        Former_Id = c.Long(nullable: false),
                        WorkGroup_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Former_Id, t.WorkGroup_Id });
            
            CreateTable(
                "dbo.FormerMeetings",
                c => new
                    {
                        Former_Id = c.Long(nullable: false),
                        Meeting_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Former_Id, t.Meeting_Id });
            
            CreateTable(
                "dbo.AdministratorMeetings",
                c => new
                    {
                        Administrator_Id = c.Long(nullable: false),
                        Meeting_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Administrator_Id, t.Meeting_Id });
            
            DropForeignKey("dbo.Meetings_Presences_Of_Trainees", "Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.Meetings_Presences_Of_Trainees", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Meetings_Presences_Of_Guests_Formers", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.Meetings_Presences_Of_Guests_Formers", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Meetings_Presences_Of_Guests_Administrators", "Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.Meetings_Presences_Of_Guests_Administrators", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Meetings_Presences_Of_Formers", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.Meetings_Presences_Of_Formers", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.Meetings_Presences_Of_Administrators", "Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.Meetings_Presences_Of_Administrators", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.WorkGroups", "Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.WorkGroups", "VicePresident_Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.WorkGroups", "VicePresident_Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroups", "VicePresident_Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.WorkGroups", "Protractor_Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.WorkGroups", "Protractor_Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroups", "Protractor_Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.WorkGroups", "President_Trainee_Id", "dbo.Trainees");
            DropForeignKey("dbo.WorkGroups", "President_Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroups", "President_Administrator_Id", "dbo.Administrators");
            DropForeignKey("dbo.WorkGroups_Memebers_Formers", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroups_Memebers_Formers", "WorkGroup_Id", "dbo.WorkGroups");
            DropForeignKey("dbo.WorkGroups", "Former_Id", "dbo.Formers");
            DropForeignKey("dbo.WorkGroups", "Administrator_Id", "dbo.Administrators");
            DropIndex("dbo.Meetings_Presences_Of_Trainees", new[] { "Trainee_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Trainees", new[] { "Meeting_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Guests_Formers", new[] { "Former_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Guests_Formers", new[] { "Meeting_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Guests_Administrators", new[] { "Administrator_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Guests_Administrators", new[] { "Meeting_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Formers", new[] { "Former_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Formers", new[] { "Meeting_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Administrators", new[] { "Administrator_Id" });
            DropIndex("dbo.Meetings_Presences_Of_Administrators", new[] { "Meeting_Id" });
            DropIndex("dbo.WorkGroups_Memebers_Formers", new[] { "Former_Id" });
            DropIndex("dbo.WorkGroups_Memebers_Formers", new[] { "WorkGroup_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Trainee_Id" });
            DropIndex("dbo.WorkGroups", new[] { "VicePresident_Trainee_Id" });
            DropIndex("dbo.WorkGroups", new[] { "VicePresident_Former_Id" });
            DropIndex("dbo.WorkGroups", new[] { "VicePresident_Administrator_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Protractor_Trainee_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Protractor_Former_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Protractor_Administrator_Id" });
            DropIndex("dbo.WorkGroups", new[] { "President_Trainee_Id" });
            DropIndex("dbo.WorkGroups", new[] { "President_Former_Id" });
            DropIndex("dbo.WorkGroups", new[] { "President_Administrator_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Former_Id" });
            DropIndex("dbo.WorkGroups", new[] { "Administrator_Id" });
            DropColumn("dbo.WorkGroups", "Trainee_Id");
            DropColumn("dbo.WorkGroups", "VicePresident_Trainee_Id");
            DropColumn("dbo.WorkGroups", "VicePresident_Former_Id");
            DropColumn("dbo.WorkGroups", "VicePresident_Administrator_Id");
            DropColumn("dbo.WorkGroups", "Protractor_Trainee_Id");
            DropColumn("dbo.WorkGroups", "Protractor_Former_Id");
            DropColumn("dbo.WorkGroups", "Protractor_Administrator_Id");
            DropColumn("dbo.WorkGroups", "President_Trainee_Id");
            DropColumn("dbo.WorkGroups", "President_Former_Id");
            DropColumn("dbo.WorkGroups", "President_Administrator_Id");
            DropColumn("dbo.WorkGroups", "Former_Id");
            DropColumn("dbo.WorkGroups", "Administrator_Id");
            DropColumn("dbo.WorkGroups", "GuestAdministrator");
            DropColumn("dbo.WorkGroups", "GuestTrainees");
            DropColumn("dbo.WorkGroups", "GuestFormers");
            DropColumn("dbo.Meetings", "Presence_Of_Protractor");
            DropColumn("dbo.Meetings", "Presence_Of_VicePresident");
            DropColumn("dbo.Meetings", "Presence_Of_President");
            DropTable("dbo.Meetings_Presences_Of_Trainees");
            DropTable("dbo.Meetings_Presences_Of_Guests_Formers");
            DropTable("dbo.Meetings_Presences_Of_Guests_Administrators");
            DropTable("dbo.Meetings_Presences_Of_Formers");
            DropTable("dbo.Meetings_Presences_Of_Administrators");
            DropTable("dbo.WorkGroups_Memebers_Formers");
            CreateIndex("dbo.FormerWorkGroups", "WorkGroup_Id");
            CreateIndex("dbo.FormerWorkGroups", "Former_Id");
            CreateIndex("dbo.FormerMeetings", "Meeting_Id");
            CreateIndex("dbo.FormerMeetings", "Former_Id");
            CreateIndex("dbo.AdministratorMeetings", "Meeting_Id");
            CreateIndex("dbo.AdministratorMeetings", "Administrator_Id");
            AddForeignKey("dbo.FormerWorkGroups", "WorkGroup_Id", "dbo.WorkGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormerWorkGroups", "Former_Id", "dbo.Formers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormerMeetings", "Meeting_Id", "dbo.Meetings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormerMeetings", "Former_Id", "dbo.Formers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdministratorMeetings", "Meeting_Id", "dbo.Meetings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdministratorMeetings", "Administrator_Id", "dbo.Administrators", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Meetings_Presences_Of_Guests_Trainees", newName: "MeetingTrainees");
            RenameTable(name: "dbo.WorkGroups_Memebers_Trainees", newName: "WorkGroupTrainees");
            RenameTable(name: "dbo.WorkGroups_Memebers_Administrators", newName: "WorkGroupAdministrators");
        }
    }
}
