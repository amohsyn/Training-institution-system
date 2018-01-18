namespace App.DAL.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.ProjectCategories",
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
                "dbo.Projects",
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
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CompletedTask = c.Boolean(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        Project_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.TaskCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        Project_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.ProjectProjectCategories",
                c => new
                    {
                        Project_Id = c.Long(nullable: false),
                        ProjectCategory_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.ProjectCategory_Id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategory_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.ProjectCategory_Id);
            
            CreateTable(
                "dbo.TaskCategoryProjectTasks",
                c => new
                    {
                        TaskCategory_Id = c.Long(nullable: false),
                        ProjectTask_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskCategory_Id, t.ProjectTask_Id })
                .ForeignKey("dbo.TaskCategories", t => t.TaskCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTasks", t => t.ProjectTask_Id, cascadeDelete: true)
                .Index(t => t.TaskCategory_Id)
                .Index(t => t.ProjectTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskCategoryProjectTasks", "ProjectTask_Id", "dbo.ProjectTasks");
            DropForeignKey("dbo.TaskCategoryProjectTasks", "TaskCategory_Id", "dbo.TaskCategories");
            DropForeignKey("dbo.TaskCategories", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectTasks", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectProjectCategories", "ProjectCategory_Id", "dbo.ProjectCategories");
            DropForeignKey("dbo.ProjectProjectCategories", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Groups", "Specialty_Id", "dbo.Specialties");
            DropIndex("dbo.TaskCategoryProjectTasks", new[] { "ProjectTask_Id" });
            DropIndex("dbo.TaskCategoryProjectTasks", new[] { "TaskCategory_Id" });
            DropIndex("dbo.ProjectProjectCategories", new[] { "ProjectCategory_Id" });
            DropIndex("dbo.ProjectProjectCategories", new[] { "Project_Id" });
            DropIndex("dbo.TaskCategories", new[] { "Project_Id" });
            DropIndex("dbo.ProjectTasks", new[] { "Project_Id" });
            DropIndex("dbo.Groups", new[] { "Specialty_Id" });
            DropTable("dbo.TaskCategoryProjectTasks");
            DropTable("dbo.ProjectProjectCategories");
            DropTable("dbo.TaskCategories");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectCategories");
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
        }
    }
}
