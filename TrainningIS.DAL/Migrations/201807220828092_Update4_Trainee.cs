namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4_Trainee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "isActif", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "isActif", c => c.Boolean(nullable: false));
        }
    }
}
