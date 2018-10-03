namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Justification_to_Absences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absences", "JustificationAbsence_Id", c => c.Long());
            CreateIndex("dbo.Absences", "JustificationAbsence_Id");
            AddForeignKey("dbo.Absences", "JustificationAbsence_Id", "dbo.JustificationAbsences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Absences", "JustificationAbsence_Id", "dbo.JustificationAbsences");
            DropIndex("dbo.Absences", new[] { "JustificationAbsence_Id" });
            DropColumn("dbo.Absences", "JustificationAbsence_Id");
        }
    }
}
