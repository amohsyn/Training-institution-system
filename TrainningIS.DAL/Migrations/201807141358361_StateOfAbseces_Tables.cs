namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateOfAbseces_Tables : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Absences", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.StateOfAbseces", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.Absences", "SeanceTrainingId", "dbo.SeanceTrainings");
            DropForeignKey("dbo.SeanceTrainings", "SeancePlanningId", "dbo.SeancePlannings");
            DropIndex("dbo.StateOfAbseces", new[] { "TraineeId" });
            DropIndex("dbo.SeanceTrainings", new[] { "SeancePlanningId" });
            DropIndex("dbo.Absences", new[] { "SeanceTrainingId" });
            DropIndex("dbo.Absences", new[] { "TraineeId" });
            DropTable("dbo.StateOfAbseces");
            DropTable("dbo.SeanceTrainings");
            DropTable("dbo.Absences");
        }
    }
}
