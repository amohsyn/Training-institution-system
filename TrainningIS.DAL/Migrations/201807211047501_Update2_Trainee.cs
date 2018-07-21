namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2_Trainee : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Trainees", "IX_Trainee_CEF");
            AlterColumn("dbo.Trainees", "CNE", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Trainees", "CNE", unique: true, name: "IX_Trainee_CEF");
            DropColumn("dbo.Trainees", "CEF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainees", "CEF", c => c.String(nullable: false, maxLength: 65));
            DropIndex("dbo.Trainees", "IX_Trainee_CEF");
            AlterColumn("dbo.Trainees", "CNE", c => c.String(nullable: false));
            CreateIndex("dbo.Trainees", "CEF", unique: true, name: "IX_Trainee_CEF");
        }
    }
}
