namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SeanceTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceTrainings", "Contained", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeanceTrainings", "Contained");
        }
    }
}
