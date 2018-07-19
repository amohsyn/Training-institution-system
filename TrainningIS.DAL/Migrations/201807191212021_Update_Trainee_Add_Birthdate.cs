namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Trainee_Add_Birthdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "Birthdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainees", "Birthdate");
        }
    }
}
