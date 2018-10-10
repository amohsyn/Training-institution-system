namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Day_Type_SeanceDay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeanceDays", "Day", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeanceDays", "Day", c => c.String());
        }
    }
}
