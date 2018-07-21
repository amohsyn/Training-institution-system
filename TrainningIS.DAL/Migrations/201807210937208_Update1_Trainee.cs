namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1_Trainee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "CEF", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Trainees", "CEF", unique: true, name: "IX_Trainee_CEF");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Trainees", "IX_Trainee_CEF");
            AlterColumn("dbo.Trainees", "CEF", c => c.String(nullable: false));
        }
    }
}
