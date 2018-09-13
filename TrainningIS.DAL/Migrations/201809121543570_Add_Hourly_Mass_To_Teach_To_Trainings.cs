namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Hourly_Mass_To_Teach_To_Trainings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "Hourly_Mass_To_Teach", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "Hourly_Mass_To_Teach");
        }
    }
}
