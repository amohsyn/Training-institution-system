namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Absence_TrainingSeance_Column : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Absences", new[] { "SeanceTrainingId" });
            AlterColumn("dbo.Absences", "SeanceTrainingId", c => c.Long(nullable: false));
            CreateIndex("dbo.Absences", "SeanceTrainingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Absences", new[] { "SeanceTrainingId" });
            AlterColumn("dbo.Absences", "SeanceTrainingId", c => c.Long());
            CreateIndex("dbo.Absences", "SeanceTrainingId");
        }
    }
}
