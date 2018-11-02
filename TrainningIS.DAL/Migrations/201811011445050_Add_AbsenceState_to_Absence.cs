namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AbsenceState_to_Absence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absences", "AbsenceState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Absences", "AbsenceState");
        }
    }
}
