namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Number_Of_Days_Of_Exclusion_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanctionCategories", "Plurality_Of_Absences", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanctionCategories", "Plurality_Of_Absences");
        }
    }
}
