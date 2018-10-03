namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Start_Time_End_Time_Form_Justification : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JustificationAbsences", "StartTime");
            DropColumn("dbo.JustificationAbsences", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JustificationAbsences", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.JustificationAbsences", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
