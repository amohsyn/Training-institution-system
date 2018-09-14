namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_SeancePlanning_From_Absences : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Absences", new[] { "SeancePlanningId" });
            RenameColumn(table: "dbo.Absences", name: "SeancePlanningId", newName: "SeancePlanning_Id");
            AlterColumn("dbo.Absences", "SeancePlanning_Id", c => c.Long());
            CreateIndex("dbo.Absences", "SeancePlanning_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Absences", new[] { "SeancePlanning_Id" });
            AlterColumn("dbo.Absences", "SeancePlanning_Id", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Absences", name: "SeancePlanning_Id", newName: "SeancePlanningId");
            CreateIndex("dbo.Absences", "SeancePlanningId");
        }
    }
}
