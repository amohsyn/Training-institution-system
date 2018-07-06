namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrainingTypeId = c.Long(nullable: false),
                        TrainingYearId = c.Long(nullable: false),
                        SpecialtyId = c.Long(nullable: false),
                        Year = c.Int(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.TrainingTypes", t => t.TrainingTypeId)
                .ForeignKey("dbo.TrainingYears", t => t.TrainingYearId)
                .Index(t => t.TrainingTypeId)
                .Index(t => t.TrainingYearId)
                .Index(t => t.SpecialtyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "TrainingYearId", "dbo.TrainingYears");
            DropForeignKey("dbo.Groups", "TrainingTypeId", "dbo.TrainingTypes");
            DropForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.Groups", new[] { "SpecialtyId" });
            DropIndex("dbo.Groups", new[] { "TrainingYearId" });
            DropIndex("dbo.Groups", new[] { "TrainingTypeId" });
            DropTable("dbo.Groups");
        }
    }
}
