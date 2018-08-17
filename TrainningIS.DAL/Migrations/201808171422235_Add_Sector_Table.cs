namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sector_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Specialties", "SectorId", c => c.Long(nullable: false));
            CreateIndex("dbo.Specialties", "SectorId");
            AddForeignKey("dbo.Specialties", "SectorId", "dbo.Sectors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specialties", "SectorId", "dbo.Sectors");
            DropIndex("dbo.Specialties", new[] { "SectorId" });
            DropColumn("dbo.Specialties", "SectorId");
            DropTable("dbo.Sectors");
        }
    }
}
