namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Specialty_to_trainee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "SpecialtyId", c => c.Long(nullable: false));
            AddColumn("dbo.Trainees", "YearStudyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Trainees", "SpecialtyId");
            CreateIndex("dbo.Trainees", "YearStudyId");
            AddForeignKey("dbo.Trainees", "SpecialtyId", "dbo.Specialties", "Id");
            AddForeignKey("dbo.Trainees", "YearStudyId", "dbo.YearStudies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "YearStudyId", "dbo.YearStudies");
            DropForeignKey("dbo.Trainees", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.Trainees", new[] { "YearStudyId" });
            DropIndex("dbo.Trainees", new[] { "SpecialtyId" });
            DropColumn("dbo.Trainees", "YearStudyId");
            DropColumn("dbo.Trainees", "SpecialtyId");
        }
    }
}
