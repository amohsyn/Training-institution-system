namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Seance_Day : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceNumbers", "Day", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeanceNumbers", "Day");
        }
    }
}
