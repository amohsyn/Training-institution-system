namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Required_Trainee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Formers", "FirstNameArabe", c => c.String());
            AlterColumn("dbo.Formers", "LastNameArabe", c => c.String());
            AlterColumn("dbo.Formers", "BirthPlace", c => c.String());
            AlterColumn("dbo.Formers", "CIN", c => c.String());
            AlterColumn("dbo.Trainees", "FirstNameArabe", c => c.String());
            AlterColumn("dbo.Trainees", "LastNameArabe", c => c.String());
            AlterColumn("dbo.Trainees", "BirthPlace", c => c.String());
            AlterColumn("dbo.Trainees", "CIN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "CIN", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "BirthPlace", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "LastNameArabe", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "FirstNameArabe", c => c.String(nullable: false));
            AlterColumn("dbo.Formers", "CIN", c => c.String(nullable: false));
            AlterColumn("dbo.Formers", "BirthPlace", c => c.String(nullable: false));
            AlterColumn("dbo.Formers", "LastNameArabe", c => c.String(nullable: false));
            AlterColumn("dbo.Formers", "FirstNameArabe", c => c.String(nullable: false));
        }
    }
}
