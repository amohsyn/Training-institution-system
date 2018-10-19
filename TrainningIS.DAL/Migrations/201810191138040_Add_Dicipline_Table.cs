namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Dicipline_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisciplineCategories",
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
            
            AddColumn("dbo.SanctionCategories", "DisciplineCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.SanctionCategories", "DisciplineCategoryId");
            AddForeignKey("dbo.SanctionCategories", "DisciplineCategoryId", "dbo.DisciplineCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanctionCategories", "DisciplineCategoryId", "dbo.DisciplineCategories");
            DropIndex("dbo.SanctionCategories", new[] { "DisciplineCategoryId" });
            DropColumn("dbo.SanctionCategories", "DisciplineCategoryId");
            DropTable("dbo.DisciplineCategories");
        }
    }
}
