namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_1_ModuleTrainingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingLevels",
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
                "dbo.Metiers",
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
            
            AddColumn("dbo.Specialties", "TrainingLevelId", c => c.Long(nullable: false));
            AddColumn("dbo.ModuleTrainings", "MetierId", c => c.Long(nullable: false));
            AddColumn("dbo.ModuleTrainings", "YearStudyId", c => c.Long(nullable: false));
            AddColumn("dbo.ModuleTrainings", "HourlyMass", c => c.Int(nullable: false));
            AddColumn("dbo.ModuleTrainings", "Hourly_Mass_To_Teach", c => c.Int(nullable: false));
            CreateIndex("dbo.Specialties", "TrainingLevelId");
            CreateIndex("dbo.ModuleTrainings", "MetierId");
            CreateIndex("dbo.ModuleTrainings", "YearStudyId");
            AddForeignKey("dbo.Specialties", "TrainingLevelId", "dbo.TrainingLevels", "Id");
            AddForeignKey("dbo.ModuleTrainings", "MetierId", "dbo.Metiers", "Id");
            AddForeignKey("dbo.ModuleTrainings", "YearStudyId", "dbo.YearStudies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModuleTrainings", "YearStudyId", "dbo.YearStudies");
            DropForeignKey("dbo.ModuleTrainings", "MetierId", "dbo.Metiers");
            DropForeignKey("dbo.Specialties", "TrainingLevelId", "dbo.TrainingLevels");
            DropIndex("dbo.ModuleTrainings", new[] { "YearStudyId" });
            DropIndex("dbo.ModuleTrainings", new[] { "MetierId" });
            DropIndex("dbo.Specialties", new[] { "TrainingLevelId" });
            DropColumn("dbo.ModuleTrainings", "Hourly_Mass_To_Teach");
            DropColumn("dbo.ModuleTrainings", "HourlyMass");
            DropColumn("dbo.ModuleTrainings", "YearStudyId");
            DropColumn("dbo.ModuleTrainings", "MetierId");
            DropColumn("dbo.Specialties", "TrainingLevelId");
            DropTable("dbo.Metiers");
            DropTable("dbo.TrainingLevels");
        }
    }
}
