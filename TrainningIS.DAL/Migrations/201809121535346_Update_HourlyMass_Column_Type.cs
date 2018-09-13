namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_HourlyMass_Column_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ModuleTrainings", "HourlyMass", c => c.Single(nullable: false));
            AlterColumn("dbo.ModuleTrainings", "Hourly_Mass_To_Teach", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ModuleTrainings", "Hourly_Mass_To_Teach", c => c.Int(nullable: false));
            AlterColumn("dbo.ModuleTrainings", "HourlyMass", c => c.Int(nullable: false));
        }
    }
}
