namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autorization_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppControllerActions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        AppControllerId = c.Long(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppControllers", t => t.AppControllerId, cascadeDelete: true)
                .Index(t => t.AppControllerId);
            
            CreateTable(
                "dbo.AppControllers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppRoleAppControllerActions",
                c => new
                    {
                        AppRole_Id = c.Long(nullable: false),
                        AppControllerAction_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppRole_Id, t.AppControllerAction_Id })
                .ForeignKey("dbo.AppRoles", t => t.AppRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.AppControllerActions", t => t.AppControllerAction_Id, cascadeDelete: true)
                .Index(t => t.AppRole_Id)
                .Index(t => t.AppControllerAction_Id);
            
            CreateTable(
                "dbo.AppRoleAppControllers",
                c => new
                    {
                        AppRole_Id = c.Long(nullable: false),
                        AppController_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppRole_Id, t.AppController_Id })
                .ForeignKey("dbo.AppRoles", t => t.AppRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.AppControllers", t => t.AppController_Id, cascadeDelete: true)
                .Index(t => t.AppRole_Id)
                .Index(t => t.AppController_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppControllerActions", "AppControllerId", "dbo.AppControllers");
            DropForeignKey("dbo.AppRoleAppControllers", "AppController_Id", "dbo.AppControllers");
            DropForeignKey("dbo.AppRoleAppControllers", "AppRole_Id", "dbo.AppRoles");
            DropForeignKey("dbo.AppRoleAppControllerActions", "AppControllerAction_Id", "dbo.AppControllerActions");
            DropForeignKey("dbo.AppRoleAppControllerActions", "AppRole_Id", "dbo.AppRoles");
            DropIndex("dbo.AppRoleAppControllers", new[] { "AppController_Id" });
            DropIndex("dbo.AppRoleAppControllers", new[] { "AppRole_Id" });
            DropIndex("dbo.AppRoleAppControllerActions", new[] { "AppControllerAction_Id" });
            DropIndex("dbo.AppRoleAppControllerActions", new[] { "AppRole_Id" });
            DropIndex("dbo.AppControllerActions", new[] { "AppControllerId" });
            DropTable("dbo.AppRoleAppControllers");
            DropTable("dbo.AppRoleAppControllerActions");
            DropTable("dbo.AppRoles");
            DropTable("dbo.AppControllers");
            DropTable("dbo.AppControllerActions");
        }
    }
}
