namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Required_Meetring_In_SanctionTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sanctions", new[] { "MeetingId" });
            RenameColumn(table: "dbo.Sanctions", name: "MeetingId", newName: "Meeting_Id");
            AlterColumn("dbo.Sanctions", "Meeting_Id", c => c.Long());
            CreateIndex("dbo.Sanctions", "Meeting_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sanctions", new[] { "Meeting_Id" });
            AlterColumn("dbo.Sanctions", "Meeting_Id", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Sanctions", name: "Meeting_Id", newName: "MeetingId");
            CreateIndex("dbo.Sanctions", "MeetingId");
        }
    }
}
