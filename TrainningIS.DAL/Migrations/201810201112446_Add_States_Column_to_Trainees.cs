namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_States_Column_to_Trainees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "States_State_XML_Date", c => c.String());
            AddColumn("dbo.Sanctions", "TraineeId", c => c.Long(nullable: false));
            CreateIndex("dbo.Sanctions", "TraineeId");
            AddForeignKey("dbo.Sanctions", "TraineeId", "dbo.Trainees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sanctions", "TraineeId", "dbo.Trainees");
            DropIndex("dbo.Sanctions", new[] { "TraineeId" });
            DropColumn("dbo.Sanctions", "TraineeId");
            DropColumn("dbo.Trainees", "States_State_XML_Date");
        }
    }
}
