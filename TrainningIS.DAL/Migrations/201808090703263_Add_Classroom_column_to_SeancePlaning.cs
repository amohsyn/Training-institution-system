namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Classroom_column_to_SeancePlaning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeancePlannings", "ClassroomId", c => c.Long(nullable: false));
            CreateIndex("dbo.SeancePlannings", "ClassroomId");
            AddForeignKey("dbo.SeancePlannings", "ClassroomId", "dbo.Classrooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeancePlannings", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.SeancePlannings", new[] { "ClassroomId" });
            DropColumn("dbo.SeancePlannings", "ClassroomId");
        }
    }
}
