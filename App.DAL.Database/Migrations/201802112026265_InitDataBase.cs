namespace App.DAL.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DashboardItemGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DashboardItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Icon = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        DashBoard_Group_Id = c.Long(),
                        FilterInfo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DashboardItemGroups", t => t.DashBoard_Group_Id)
                .ForeignKey("dbo.FilterInfoes", t => t.FilterInfo_Id)
                .Index(t => t.DashBoard_Group_Id)
                .Index(t => t.FilterInfo_Id);
            
            CreateTable(
                "dbo.FilterInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        isDefaultFilter = c.Boolean(nullable: false),
                        Description = c.String(),
                        FilterString = c.String(),
                        SortString = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        ManagerInfo_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManagerInfoes", t => t.ManagerInfo_Id, cascadeDelete: true)
                .Index(t => t.ManagerInfo_Id);
            
            CreateTable(
                "dbo.ManagerInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        isSystem = c.Boolean(nullable: false),
                        Form_AssemblyName = c.String(nullable: false),
                        Form_FullTypeName = c.String(nullable: false),
                        Grid_AssemblyName = c.String(nullable: false),
                        Gird_FullTypeName = c.String(nullable: false),
                        BLO_AssemblyName = c.String(nullable: false),
                        BLO_FullTypeName = c.String(nullable: false),
                        Entity_AssemblyName = c.String(nullable: false),
                        Entity_FullTypeName = c.String(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Code = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        Specialty_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.Specialty_Id)
                .Index(t => t.Specialty_Id);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Code = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "Specialty_Id", "dbo.Specialties");
            DropForeignKey("dbo.DashboardItems", "FilterInfo_Id", "dbo.FilterInfoes");
            DropForeignKey("dbo.FilterInfoes", "ManagerInfo_Id", "dbo.ManagerInfoes");
            DropForeignKey("dbo.DashboardItems", "DashBoard_Group_Id", "dbo.DashboardItemGroups");
            DropIndex("dbo.Groups", new[] { "Specialty_Id" });
            DropIndex("dbo.FilterInfoes", new[] { "ManagerInfo_Id" });
            DropIndex("dbo.DashboardItems", new[] { "FilterInfo_Id" });
            DropIndex("dbo.DashboardItems", new[] { "DashBoard_Group_Id" });
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
            DropTable("dbo.ManagerInfoes");
            DropTable("dbo.FilterInfoes");
            DropTable("dbo.DashboardItems");
            DropTable("dbo.DashboardItemGroups");
        }
    }
}
