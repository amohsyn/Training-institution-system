namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Justification_to_Sanction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sanctions", "JustificationAbsence_Id", c => c.Long());
            CreateIndex("dbo.Sanctions", "JustificationAbsence_Id");
            AddForeignKey("dbo.Sanctions", "JustificationAbsence_Id", "dbo.JustificationAbsences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sanctions", "JustificationAbsence_Id", "dbo.JustificationAbsences");
            DropIndex("dbo.Sanctions", new[] { "JustificationAbsence_Id" });
            DropColumn("dbo.Sanctions", "JustificationAbsence_Id");
        }
    }
}
