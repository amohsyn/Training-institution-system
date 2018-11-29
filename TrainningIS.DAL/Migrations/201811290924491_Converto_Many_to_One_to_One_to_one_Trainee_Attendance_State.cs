namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Converto_Many_to_One_to_One_to_one_Trainee_Attendance_State : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Mission_Working_GroupWorkGroup", newName: "WorkGroupMission_Working_Group");
            DropPrimaryKey("dbo.WorkGroupMission_Working_Group");
            CreateTable(
                "dbo.AttendanceStates",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Valid_Note = c.Single(nullable: false),
                        Invalid_Note = c.Single(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Invalid_Sanction_Id = c.Long(),
                        Valid_Sanction_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sanctions", t => t.Invalid_Sanction_Id)
                .ForeignKey("dbo.Trainees", t => t.Id)
                .ForeignKey("dbo.Sanctions", t => t.Valid_Sanction_Id)
                .Index(t => t.Id)
                .Index(t => t.Invalid_Sanction_Id)
                .Index(t => t.Valid_Sanction_Id);
            
            AddPrimaryKey("dbo.WorkGroupMission_Working_Group", new[] { "WorkGroup_Id", "Mission_Working_Group_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceStates", "Valid_Sanction_Id", "dbo.Sanctions");
            DropForeignKey("dbo.AttendanceStates", "Id", "dbo.Trainees");
            DropForeignKey("dbo.AttendanceStates", "Invalid_Sanction_Id", "dbo.Sanctions");
            DropIndex("dbo.AttendanceStates", new[] { "Valid_Sanction_Id" });
            DropIndex("dbo.AttendanceStates", new[] { "Invalid_Sanction_Id" });
            DropIndex("dbo.AttendanceStates", new[] { "Id" });
            DropPrimaryKey("dbo.WorkGroupMission_Working_Group");
            DropTable("dbo.AttendanceStates");
            AddPrimaryKey("dbo.WorkGroupMission_Working_Group", new[] { "Mission_Working_Group_Id", "WorkGroup_Id" });
            RenameTable(name: "dbo.WorkGroupMission_Working_Group", newName: "Mission_Working_GroupWorkGroup");
        }
    }
}
