namespace TrainingIS.DAL.Migrations
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
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        SpecialtyId = c.Long(),
                        TrainingYearId = c.Long(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.TrainingYears", t => t.TrainingYearId)
                .Index(t => t.SpecialtyId)
                .Index(t => t.TrainingYearId);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingYears",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndtDate = c.DateTime(nullable: false),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.Boolean(nullable: false),
                        CIN = c.String(),
                        CNE = c.String(),
                        Cellphone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        GroupId = c.Long(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "TrainingYearId", "dbo.TrainingYears");
            DropForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.Trainees", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "TrainingYearId" });
            DropIndex("dbo.Groups", new[] { "SpecialtyId" });
            DropTable("dbo.Trainees");
            DropTable("dbo.TrainingYears");
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
        }
    }
}
