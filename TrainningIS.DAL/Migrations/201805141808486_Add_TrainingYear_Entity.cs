namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TrainingYear_Entity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "Specialty_Id", "dbo.Specialties");
            DropIndex("dbo.Groups", new[] { "Specialty_Id" });
            RenameColumn(table: "dbo.Groups", name: "Specialty_Id", newName: "SpecialtyId");
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
            
            AddColumn("dbo.Groups", "TrainingYearId", c => c.Long(nullable: false));
            AlterColumn("dbo.Groups", "SpecialtyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Groups", "SpecialtyId");
            CreateIndex("dbo.Groups", "TrainingYearId");
            AddForeignKey("dbo.Groups", "TrainingYearId", "dbo.TrainingYears", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.Groups", "TrainingYearId", "dbo.TrainingYears");
            DropIndex("dbo.Groups", new[] { "TrainingYearId" });
            DropIndex("dbo.Groups", new[] { "SpecialtyId" });
            AlterColumn("dbo.Groups", "SpecialtyId", c => c.Long());
            DropColumn("dbo.Groups", "TrainingYearId");
            DropTable("dbo.TrainingYears");
            RenameColumn(table: "dbo.Groups", name: "SpecialtyId", newName: "Specialty_Id");
            CreateIndex("dbo.Groups", "Specialty_Id");
            AddForeignKey("dbo.Groups", "Specialty_Id", "dbo.Specialties", "Id");
        }
    }
}
