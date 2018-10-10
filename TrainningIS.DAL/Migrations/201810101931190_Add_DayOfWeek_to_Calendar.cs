namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DayOfWeek_to_Calendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarDays", "DayOfWeek", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarDays", "DayOfWeek");
        }
    }
}
