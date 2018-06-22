namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainingTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingTypes",
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
            
            AddColumn("dbo.Groups", "TrainingTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.Groups", "Year", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "TrainingTypeId");
            AddForeignKey("dbo.Groups", "TrainingTypeId", "dbo.TrainingTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "TrainingTypeId", "dbo.TrainingTypes");
            DropIndex("dbo.Groups", new[] { "TrainingTypeId" });
            DropColumn("dbo.Groups", "Year");
            DropColumn("dbo.Groups", "TrainingTypeId");
            DropTable("dbo.TrainingTypes");
        }
    }
}
