namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attendance_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceStates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TraineeId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Trainees", t => t.TraineeId)
                .ForeignKey("dbo.Sanctions", t => t.Valid_Sanction_Id)
                .Index(t => t.TraineeId)
                .Index(t => t.Invalid_Sanction_Id)
                .Index(t => t.Valid_Sanction_Id);
            
            AddColumn("dbo.Sanctions", "SanctionState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceStates", "Valid_Sanction_Id", "dbo.Sanctions");
            DropForeignKey("dbo.AttendanceStates", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.AttendanceStates", "Invalid_Sanction_Id", "dbo.Sanctions");
            DropIndex("dbo.AttendanceStates", new[] { "Valid_Sanction_Id" });
            DropIndex("dbo.AttendanceStates", new[] { "Invalid_Sanction_Id" });
            DropIndex("dbo.AttendanceStates", new[] { "TraineeId" });
            DropColumn("dbo.Sanctions", "SanctionState");
            DropTable("dbo.AttendanceStates");
        }
    }
}
