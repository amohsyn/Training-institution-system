namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Avertissement_And_WarningTrainee_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category_JustificationAbsence",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category_WarningTrainee",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JustificationAbsences",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TraineeId = c.Long(nullable: false),
                        Category_JustificationAbsenceId = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndtDate = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category_JustificationAbsence", t => t.Category_JustificationAbsenceId)
                .ForeignKey("dbo.Trainees", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.Category_JustificationAbsenceId);
            
            CreateTable(
                "dbo.WarningTrainees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TraineeId = c.Long(nullable: false),
                        WarningDate = c.DateTime(nullable: false),
                        Category_WarningTraineeId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category_WarningTrainee", t => t.Category_WarningTraineeId)
                .ForeignKey("dbo.Trainees", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.Category_WarningTraineeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarningTrainees", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.WarningTrainees", "Category_WarningTraineeId", "dbo.Category_WarningTrainee");
            DropForeignKey("dbo.JustificationAbsences", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.JustificationAbsences", "Category_JustificationAbsenceId", "dbo.Category_JustificationAbsence");
            DropIndex("dbo.WarningTrainees", new[] { "Category_WarningTraineeId" });
            DropIndex("dbo.WarningTrainees", new[] { "TraineeId" });
            DropIndex("dbo.JustificationAbsences", new[] { "Category_JustificationAbsenceId" });
            DropIndex("dbo.JustificationAbsences", new[] { "TraineeId" });
            DropTable("dbo.WarningTrainees");
            DropTable("dbo.JustificationAbsences");
            DropTable("dbo.Category_WarningTrainee");
            DropTable("dbo.Category_JustificationAbsence");
        }
    }
}
