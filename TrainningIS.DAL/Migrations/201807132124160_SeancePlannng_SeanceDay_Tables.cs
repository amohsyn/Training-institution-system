namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeancePlannng_SeanceDay_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeanceDays",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeancePlannings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingId = c.Long(nullable: false),
                        SeanceDayId = c.Long(nullable: false),
                        SeanceNumberId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeanceDays", t => t.SeanceDayId)
                .ForeignKey("dbo.SeanceNumbers", t => t.SeanceNumberId)
                .ForeignKey("dbo.Trainings", t => t.TrainingId)
                .Index(t => t.TrainingId)
                .Index(t => t.SeanceDayId)
                .Index(t => t.SeanceNumberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeancePlannings", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.SeancePlannings", "SeanceNumberId", "dbo.SeanceNumbers");
            DropForeignKey("dbo.SeancePlannings", "SeanceDayId", "dbo.SeanceDays");
            DropIndex("dbo.SeancePlannings", new[] { "SeanceNumberId" });
            DropIndex("dbo.SeancePlannings", new[] { "SeanceDayId" });
            DropIndex("dbo.SeancePlannings", new[] { "TrainingId" });
            DropTable("dbo.SeancePlannings");
            DropTable("dbo.SeanceDays");
        }
    }
}
