namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_System_DisciplineCategories_to_DisciplineCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineCategories", "System_DisciplineCategy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DisciplineCategories", "System_DisciplineCategy");
        }
    }
}
