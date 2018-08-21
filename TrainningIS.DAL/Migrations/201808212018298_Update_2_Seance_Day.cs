namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_2_Seance_Day : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceDays", "Day", c => c.String());
            DropColumn("dbo.SeanceNumbers", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeanceNumbers", "Day", c => c.String());
            DropColumn("dbo.SeanceDays", "Day");
        }
    }
}
