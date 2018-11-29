namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DROP_AttendanceStates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceStates", "Invalid_Sanction_Id", "dbo.Sanctions");
            DropForeignKey("dbo.AttendanceStates", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.AttendanceStates", "Valid_Sanction_Id", "dbo.Sanctions");
            DropIndex("dbo.AttendanceStates", new[] { "TraineeId" });
            DropIndex("dbo.AttendanceStates", new[] { "Invalid_Sanction_Id" });
            DropIndex("dbo.AttendanceStates", new[] { "Valid_Sanction_Id" });
            DropTable("dbo.AttendanceStates");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AttendanceStates", "Valid_Sanction_Id");
            CreateIndex("dbo.AttendanceStates", "Invalid_Sanction_Id");
            CreateIndex("dbo.AttendanceStates", "TraineeId");
            AddForeignKey("dbo.AttendanceStates", "Valid_Sanction_Id", "dbo.Sanctions", "Id");
            AddForeignKey("dbo.AttendanceStates", "TraineeId", "dbo.Trainees", "Id");
            AddForeignKey("dbo.AttendanceStates", "Invalid_Sanction_Id", "dbo.Sanctions", "Id");
        }
    }
}
