namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GPicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GPictures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Original_Thumbnail = c.String(),
                        Large_Thumbnail = c.String(),
                        Medium_Thumbnail = c.String(),
                        Small_Thumbnail = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trainees", "Photo_Id", c => c.Long());
            AddColumn("dbo.Formers", "Photo_Id", c => c.Long());
            CreateIndex("dbo.Trainees", "Photo_Id");
            CreateIndex("dbo.Formers", "Photo_Id");
            AddForeignKey("dbo.Trainees", "Photo_Id", "dbo.GPictures", "Id");
            AddForeignKey("dbo.Formers", "Photo_Id", "dbo.GPictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Formers", "Photo_Id", "dbo.GPictures");
            DropForeignKey("dbo.Trainees", "Photo_Id", "dbo.GPictures");
            DropIndex("dbo.Formers", new[] { "Photo_Id" });
            DropIndex("dbo.Trainees", new[] { "Photo_Id" });
            DropColumn("dbo.Formers", "Photo_Id");
            DropColumn("dbo.Trainees", "Photo_Id");
            DropTable("dbo.GPictures");
        }
    }
}
