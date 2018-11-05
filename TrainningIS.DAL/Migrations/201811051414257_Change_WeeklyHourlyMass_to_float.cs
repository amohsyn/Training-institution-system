namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_WeeklyHourlyMass_to_float : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Formers", "WeeklyHourlyMass", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Formers", "WeeklyHourlyMass", c => c.Int(nullable: false));
        }
    }
}
