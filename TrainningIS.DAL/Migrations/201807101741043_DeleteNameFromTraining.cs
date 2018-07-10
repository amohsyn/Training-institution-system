namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNameFromTraining : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trainings", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "Name", c => c.String(nullable: false));
        }
    }
}
