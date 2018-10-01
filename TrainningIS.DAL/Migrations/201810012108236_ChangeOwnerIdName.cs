namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOwnerIdName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "Owner_Id", newName: "OwnerId");
            RenameColumn(table: "dbo.TaskProjects", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Projects", name: "IX_Owner_Id", newName: "IX_OwnerId");
            RenameIndex(table: "dbo.TaskProjects", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TaskProjects", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameIndex(table: "dbo.Projects", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.TaskProjects", name: "OwnerId", newName: "Owner_Id");
            RenameColumn(table: "dbo.Projects", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
