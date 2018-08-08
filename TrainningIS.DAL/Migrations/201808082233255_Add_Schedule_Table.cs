namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Schedule_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingYearId = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndtDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainingYears", t => t.TrainingYearId)
                .Index(t => t.TrainingYearId);
            
            AddColumn("dbo.SeancePlannings", "ScheduleId", c => c.Long(nullable: false));
            CreateIndex("dbo.SeancePlannings", "ScheduleId");
            AddForeignKey("dbo.SeancePlannings", "ScheduleId", "dbo.Schedules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeancePlannings", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "TrainingYearId", "dbo.TrainingYears");
            DropIndex("dbo.Schedules", new[] { "TrainingYearId" });
            DropIndex("dbo.SeancePlannings", new[] { "ScheduleId" });
            DropColumn("dbo.SeancePlannings", "ScheduleId");
            DropTable("dbo.Schedules");
        }
    }
}
