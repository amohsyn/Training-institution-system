namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Classroom_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(),
                        ClassroomCategoryId = c.Long(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassroomCategories", t => t.ClassroomCategoryId)
                .Index(t => t.ClassroomCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classrooms", "ClassroomCategoryId", "dbo.ClassroomCategories");
            DropIndex("dbo.Classrooms", new[] { "ClassroomCategoryId" });
            DropTable("dbo.Classrooms");
            DropTable("dbo.ClassroomCategories");
        }
    }
}
