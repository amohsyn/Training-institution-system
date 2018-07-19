namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1_Trainee_SchoolLevel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Trainees", new[] { "SchoollevelId" });
            AlterColumn("dbo.Trainees", "SchoollevelId", c => c.Long());
            CreateIndex("dbo.Trainees", "SchoollevelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Trainees", new[] { "SchoollevelId" });
            AlterColumn("dbo.Trainees", "SchoollevelId", c => c.Long(nullable: false));
            CreateIndex("dbo.Trainees", "SchoollevelId");
        }
    }
}
