namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ControllerApp_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActionControllerApps", "ControllerApp_Id", "dbo.ControllerApps");
            DropForeignKey("dbo.AuthrorizationApps", "ControllerApp_Id", "dbo.ControllerApps");
            DropIndex("dbo.ActionControllerApps", new[] { "ControllerApp_Id" });
            DropIndex("dbo.AuthrorizationApps", new[] { "ControllerApp_Id" });
            RenameColumn(table: "dbo.ActionControllerApps", name: "ControllerApp_Id", newName: "ControllerAppId");
            RenameColumn(table: "dbo.AuthrorizationApps", name: "ControllerApp_Id", newName: "ControllerAppId");
            AlterColumn("dbo.ActionControllerApps", "ControllerAppId", c => c.Long(nullable: false));
            AlterColumn("dbo.AuthrorizationApps", "ControllerAppId", c => c.Long(nullable: false));
            CreateIndex("dbo.ActionControllerApps", "ControllerAppId");
            CreateIndex("dbo.AuthrorizationApps", "ControllerAppId");
            AddForeignKey("dbo.ActionControllerApps", "ControllerAppId", "dbo.ControllerApps", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthrorizationApps", "ControllerAppId", "dbo.ControllerApps", "Id", cascadeDelete: true);
            DropColumn("dbo.ActionControllerApps", "AppControllerId");
            DropColumn("dbo.AuthrorizationApps", "AppControllerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthrorizationApps", "AppControllerId", c => c.Long(nullable: false));
            AddColumn("dbo.ActionControllerApps", "AppControllerId", c => c.Long(nullable: false));
            DropForeignKey("dbo.AuthrorizationApps", "ControllerAppId", "dbo.ControllerApps");
            DropForeignKey("dbo.ActionControllerApps", "ControllerAppId", "dbo.ControllerApps");
            DropIndex("dbo.AuthrorizationApps", new[] { "ControllerAppId" });
            DropIndex("dbo.ActionControllerApps", new[] { "ControllerAppId" });
            AlterColumn("dbo.AuthrorizationApps", "ControllerAppId", c => c.Long());
            AlterColumn("dbo.ActionControllerApps", "ControllerAppId", c => c.Long());
            RenameColumn(table: "dbo.AuthrorizationApps", name: "ControllerAppId", newName: "ControllerApp_Id");
            RenameColumn(table: "dbo.ActionControllerApps", name: "ControllerAppId", newName: "ControllerApp_Id");
            CreateIndex("dbo.AuthrorizationApps", "ControllerApp_Id");
            CreateIndex("dbo.ActionControllerApps", "ControllerApp_Id");
            AddForeignKey("dbo.AuthrorizationApps", "ControllerApp_Id", "dbo.ControllerApps", "Id");
            AddForeignKey("dbo.ActionControllerApps", "ControllerApp_Id", "dbo.ControllerApps", "Id");
        }
    }
}
