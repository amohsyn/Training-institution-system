namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3_Trainee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "Sex", c => c.Boolean(nullable: false));
        }
    }
}
