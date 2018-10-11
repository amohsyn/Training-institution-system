namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Duration_To_SeanceTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceTrainings", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeanceTrainings", "Duration");
        }
    }
}
