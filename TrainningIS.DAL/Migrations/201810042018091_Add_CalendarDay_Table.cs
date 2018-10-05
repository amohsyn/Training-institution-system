namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CalendarDay_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarDays",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DateName = c.String(),
                        DateNameAbbrev = c.String(),
                        IsWeekend = c.Boolean(nullable: false),
                        WeekNumber = c.Int(nullable: false),
                        WeekBeginDate = c.DateTime(nullable: false),
                        WeekEndDate = c.DateTime(nullable: false),
                        CalendarMonthName = c.String(),
                        CalendarMonthNameAbbrev = c.String(),
                        CalendarMonthBegin = c.DateTime(nullable: false),
                        CalendarMonthEnd = c.DateTime(nullable: false),
                        CalendarMonthNumber = c.Int(nullable: false),
                        CalendarYear = c.Int(nullable: false),
                        FiscalYear = c.Int(nullable: false),
                        DayOfYear = c.Int(nullable: false),
                        CalendarYearBegin = c.DateTime(nullable: false),
                        CalendarYearEnd = c.DateTime(nullable: false),
                        FiscalYearBegin = c.DateTime(nullable: false),
                        FiscalYearEnd = c.DateTime(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CalendarDays");
        }
    }
}
