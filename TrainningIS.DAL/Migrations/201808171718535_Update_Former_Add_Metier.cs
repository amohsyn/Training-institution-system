namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Former_Add_Metier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formers", "MetierId", c => c.Long(nullable: false));
            AddColumn("dbo.Formers", "WeeklyHourlyMass", c => c.Int(nullable: false));
            CreateIndex("dbo.Formers", "MetierId");
            AddForeignKey("dbo.Formers", "MetierId", "dbo.Metiers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Formers", "MetierId", "dbo.Metiers");
            DropIndex("dbo.Formers", new[] { "MetierId" });
            DropColumn("dbo.Formers", "WeeklyHourlyMass");
            DropColumn("dbo.Formers", "MetierId");
        }
    }
}
