namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Absence_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absences", "AbsenceDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Absences", "SeancePlanningId", c => c.Long(nullable: false));
            CreateIndex("dbo.Absences", "SeancePlanningId");
            AddForeignKey("dbo.Absences", "SeancePlanningId", "dbo.SeancePlannings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Absences", "SeancePlanningId", "dbo.SeancePlannings");
            DropIndex("dbo.Absences", new[] { "SeancePlanningId" });
            DropColumn("dbo.Absences", "SeancePlanningId");
            DropColumn("dbo.Absences", "AbsenceDate");
        }
    }
}
