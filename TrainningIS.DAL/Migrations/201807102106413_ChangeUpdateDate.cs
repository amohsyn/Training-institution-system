namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUpdateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassroomCategories", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ClassroomCategories", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classrooms", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classrooms", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Formers", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Formers", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Specialties", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Specialties", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingTypes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingTypes", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingYears", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingYears", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ModuleTrainings", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ModuleTrainings", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainees", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainees", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainings", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainings", "UpdateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ClassroomCategories", "DateCreation");
            DropColumn("dbo.ClassroomCategories", "DateModification");
            DropColumn("dbo.Classrooms", "DateCreation");
            DropColumn("dbo.Classrooms", "DateModification");
            DropColumn("dbo.Formers", "DateCreation");
            DropColumn("dbo.Formers", "DateModification");
            DropColumn("dbo.Groups", "DateCreation");
            DropColumn("dbo.Groups", "DateModification");
            DropColumn("dbo.Specialties", "DateCreation");
            DropColumn("dbo.Specialties", "DateModification");
            DropColumn("dbo.TrainingTypes", "DateCreation");
            DropColumn("dbo.TrainingTypes", "DateModification");
            DropColumn("dbo.TrainingYears", "DateCreation");
            DropColumn("dbo.TrainingYears", "DateModification");
            DropColumn("dbo.ModuleTrainings", "DateCreation");
            DropColumn("dbo.ModuleTrainings", "DateModification");
            DropColumn("dbo.Trainees", "DateCreation");
            DropColumn("dbo.Trainees", "DateModification");
            DropColumn("dbo.Trainings", "DateCreation");
            DropColumn("dbo.Trainings", "DateModification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainings", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainees", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainees", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.ModuleTrainings", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.ModuleTrainings", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingYears", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingYears", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingTypes", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrainingTypes", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Specialties", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Specialties", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Formers", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Formers", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classrooms", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classrooms", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("dbo.ClassroomCategories", "DateModification", c => c.DateTime(nullable: false));
            AddColumn("dbo.ClassroomCategories", "DateCreation", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trainings", "UpdateDate");
            DropColumn("dbo.Trainings", "CreateDate");
            DropColumn("dbo.Trainees", "UpdateDate");
            DropColumn("dbo.Trainees", "CreateDate");
            DropColumn("dbo.ModuleTrainings", "UpdateDate");
            DropColumn("dbo.ModuleTrainings", "CreateDate");
            DropColumn("dbo.TrainingYears", "UpdateDate");
            DropColumn("dbo.TrainingYears", "CreateDate");
            DropColumn("dbo.TrainingTypes", "UpdateDate");
            DropColumn("dbo.TrainingTypes", "CreateDate");
            DropColumn("dbo.Specialties", "UpdateDate");
            DropColumn("dbo.Specialties", "CreateDate");
            DropColumn("dbo.Groups", "UpdateDate");
            DropColumn("dbo.Groups", "CreateDate");
            DropColumn("dbo.Formers", "UpdateDate");
            DropColumn("dbo.Formers", "CreateDate");
            DropColumn("dbo.Classrooms", "UpdateDate");
            DropColumn("dbo.Classrooms", "CreateDate");
            DropColumn("dbo.ClassroomCategories", "UpdateDate");
            DropColumn("dbo.ClassroomCategories", "CreateDate");
        }
    }
}
