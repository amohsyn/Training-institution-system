namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trainigs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingYearId = c.Long(nullable: false),
                        ModuleTrainingId = c.Long(nullable: false),
                        FormerId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
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
            
            AlterColumn("dbo.ModuleTrainings", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "TrainingYearId", "dbo.TrainingYears");
            DropForeignKey("dbo.Trainings", "ModuleTrainingId", "dbo.ModuleTrainings");
            DropForeignKey("dbo.Trainings", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Trainings", "FormerId", "dbo.Formers");
            DropIndex("dbo.Trainings", new[] { "GroupId" });
            DropIndex("dbo.Trainings", new[] { "FormerId" });
            DropIndex("dbo.Trainings", new[] { "ModuleTrainingId" });
            DropIndex("dbo.Trainings", new[] { "TrainingYearId" });
            AlterColumn("dbo.ModuleTrainings", "Code", c => c.String(nullable: false));
            DropTable("dbo.Trainings");
        }
    }
}
