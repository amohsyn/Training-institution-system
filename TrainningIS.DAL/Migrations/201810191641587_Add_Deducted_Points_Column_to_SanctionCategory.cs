namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Deducted_Points_Column_to_SanctionCategory : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sanctions", new[] { "Meeting_Id" });
            RenameColumn(table: "dbo.Sanctions", name: "Meeting_Id", newName: "MeetingId");
            AddColumn("dbo.SanctionCategories", "Deducted_Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Sanctions", "MeetingId", c => c.Long(nullable: false));
            CreateIndex("dbo.Sanctions", "MeetingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sanctions", new[] { "MeetingId" });
            AlterColumn("dbo.Sanctions", "MeetingId", c => c.Long());
            DropColumn("dbo.SanctionCategories", "Deducted_Points");
            RenameColumn(table: "dbo.Sanctions", name: "MeetingId", newName: "Meeting_Id");
            CreateIndex("dbo.Sanctions", "Meeting_Id");
        }
    }
}
