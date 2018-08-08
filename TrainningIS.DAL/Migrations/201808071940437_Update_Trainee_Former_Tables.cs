namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Trainee_Former_Tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formers", "FirstNameArabe", c => c.String(nullable: false));
            AddColumn("dbo.Formers", "LastNameArabe", c => c.String(nullable: false));
            AddColumn("dbo.Formers", "Birthdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Formers", "NationalityId", c => c.Long(nullable: false));
            AddColumn("dbo.Formers", "BirthPlace", c => c.String(nullable: false));
            AlterColumn("dbo.Formers", "Sex", c => c.Int(nullable: false));
            AlterColumn("dbo.Formers", "CIN", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "Email", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Formers", "NationalityId");
            CreateIndex("dbo.Trainees", "Email", unique: true, name: "IX_Former_Email");
            AddForeignKey("dbo.Formers", "NationalityId", "dbo.Nationalities", "Id");
            DropColumn("dbo.Trainees", "TutorCellPhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainees", "TutorCellPhone", c => c.String());
            DropForeignKey("dbo.Formers", "NationalityId", "dbo.Nationalities");
            DropIndex("dbo.Trainees", "IX_Former_Email");
            DropIndex("dbo.Formers", new[] { "NationalityId" });
            AlterColumn("dbo.Trainees", "Email", c => c.String());
            AlterColumn("dbo.Formers", "CIN", c => c.String());
            AlterColumn("dbo.Formers", "Sex", c => c.Boolean(nullable: false));
            DropColumn("dbo.Formers", "BirthPlace");
            DropColumn("dbo.Formers", "NationalityId");
            DropColumn("dbo.Formers", "Birthdate");
            DropColumn("dbo.Formers", "LastNameArabe");
            DropColumn("dbo.Formers", "FirstNameArabe");
        }
    }
}
