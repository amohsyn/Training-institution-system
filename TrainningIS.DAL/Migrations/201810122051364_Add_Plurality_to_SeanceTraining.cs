namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Plurality_to_SeanceTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceTrainings", "Plurality", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeanceTrainings", "Plurality");
        }
    }
}
